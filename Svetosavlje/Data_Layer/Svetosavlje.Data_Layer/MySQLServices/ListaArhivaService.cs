using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer.Core;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


namespace Svetosavlje.Data_Layer.MySQLServices
{
    public class ListaArhivaService : IListaArhiva
    {
        private dbConnection dbConn = new dbConnection();

        public IList<MessageThread> GetMessageThreads(int rows)
        {
            return GetMessageThreads(0, rows);
        }



        public IList<MessageThread> GetMessageThreads(int page, int rows)
        {
            IList<MessageThread> returnList = new List<MessageThread>();
            string strSQL = @"SELECT topics.ID, topics.naziv, count(messages.temaID) AS cnt, users.ime, topics.updated, users.email, topics.last_user
                  FROM topics 
                     INNER JOIN users  ON topics.last_user = users.ID 
                     INNER JOIN messages ON topics.ID = messages.temaID
                  GROUP BY messages.temaID 
                  ORDER BY topics.updated DESC ";
            strSQL += "LIMIT " + (page * rows).ToString() + "," + rows.ToString();

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.ListaArhiva);

            foreach (DataRow row in list.Rows)
            {
                int msgId = Convert.ToInt32(row["ID"]);
                string title = row["naziv"].ToString();
                if (string.IsNullOrEmpty(title))
                    title = "(unknown title)";
                int count = Convert.ToInt32(row["cnt"]);
                string user = row["ime"].ToString();
                if (string.IsNullOrEmpty(user))
                    user = row["email"].ToString();
                DateTime updated = Convert.ToDateTime(row["updated"]);
                int updId = Convert.ToInt32(row["last_user"]);
                MessageThread ti = new MessageThread(msgId, title, count, user, updated, updId);
                returnList.Add(ti);
            }

            return returnList;
        }

        public int GetTotalTopics()
        {
            string strSQL = @"SELECT count(topics.ID) as cnt FROM topics";

            return Convert.ToInt32(dbConn.GetScalar(strSQL, dbConnection.Connenction.ListaArhiva));
        }


        public IList<TopicMessage> GetTopicMessages(int topicID)
        {
            string strSQL = @"SELECT messages.ID As msgID, users.email, users.ime, messages.datum, messages.htmlText, users.ID As userID, messages.InReplyTo, messages.emailHeader 
                                FROM messages INNER JOIN users  ON messages.userID = users.ID  
                                WHERE messages.temaID = " + topicID + " ORDER BY messages.datum ASC";
            
            List<TopicMessage> topicMessages = new List<TopicMessage>();

            DataTable msgs = dbConn.GetDataTable(strSQL, dbConnection.Connenction.ListaArhiva);
            foreach (DataRow row in msgs.Rows)
            {
                int msgId = Convert.ToInt32(row["msgID"]);
                string email = row["email"].ToString();
                if (string.IsNullOrEmpty(email))
                    email = "(unknown email)";
                string name = row["ime"].ToString();
                if (string.IsNullOrEmpty(name))
                    name = email;
                DateTime time = Convert.ToDateTime(row["datum"]);
                string htmlText = row["htmlText"].ToString();
                int usrId = Convert.ToInt32(row["userID"]);
                int inReplyTo = Convert.ToInt32(row["InReplyTo"]);
                string emailHeader = row["emailHeader"].ToString();

                List<AttachmentInfo> attachs = GetAttachmentInfos(msgId);

                TopicMessage tm = new TopicMessage(msgId, usrId, name, time, htmlText, attachs, inReplyTo, emailHeader);
                topicMessages.Add(tm);
            }


            
            FilterTopicMessageText(topicMessages);

            return topicMessages;
        }

        private List<AttachmentInfo> GetAttachmentInfos(int messageId)
        {
            string strSQL = "SELECT ID, title, type, LENGTH(content) As AttLen FROM attachments WHERE messageID = " + messageId;

            List<AttachmentInfo> attList = new List<AttachmentInfo>();

            DataTable attachments = dbConn.GetDataTable(strSQL, dbConnection.Connenction.ListaArhiva);
            foreach (DataRow row in attachments.Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string title = row["title"].ToString();
                string type = row["type"].ToString();
                long size = Convert.ToInt64(row["AttLen"]);
                AttachmentInfo att = new AttachmentInfo(id, title, type, size);
                attList.Add(att);
            }

            return attList;
        }

        private void FilterTopicMessageText(List<TopicMessage> topicList)
        {
            Dictionary<string, int> word2int = new Dictionary<string, int>();
            word2int.Add(" ", 0);

            List<string> int2word = new List<string>();
            int2word.Add(" ");

            int wordId = 1;
            foreach (TopicMessage tm in topicList)
            {
                wordId = PrepareWordCodes(tm, word2int, int2word, wordId);
            }

            for (int i = 0; i < topicList.Count; i++)
            {
                topicList[i].InReplyTo = null;  // MessageInfo GetMessage(int msgID)
                for (int j = i - 1; j >= 0; j--)
                {
                    if (topicList[i].inReplyTo == topicList[j].messageID)
                    {
                        topicList[i].InReplyTo = topicList[j];
                        break;
                    }
                }
                if ((topicList[i].InReplyTo == null) && (topicList[i].inReplyTo != 0))
                {
                    string inReplyToText = GetMessageText(topicList[i].inReplyTo);
                    if (inReplyToText != null)
                    {
                        topicList[i].InReplyTo = new TopicMessage(topicList[i].inReplyTo, inReplyToText);
                        wordId = PrepareWordCodes(topicList[i].InReplyTo, word2int, int2word, wordId);
                    }
                }
            }

            string[] wordArray = int2word.ToArray();
            
            Diff diff = new Diff();

            foreach (TopicMessage tm in topicList)
            {
                if (tm.InReplyTo != null)
                {
                    Diff.Item[] diffItems = diff.DiffInt(tm.InReplyTo.WordCodes, tm.WordCodes);

                    int len = tm.WordCodes.Length;
                    StringBuilder sb = new StringBuilder();
                    int pos = 0;
                    int succesiveBrCount = 0;
                    foreach (Diff.Item item in diffItems)
                    {
                        if (Math.Min(item.StartB, len) - pos > 10)
                        {
                            sb.Append("<br/><br/>[...]");
                            break;
                        }

                        // write unchanged words
                        while ((pos < item.StartB) && (pos < len))
                        {
                            sb.Append(wordArray[tm.WordCodes[pos]]);
                            sb.Append(' ');
                            ++pos;
                        }

                        // write inserted words
                        if (pos < item.StartB + item.insertedB)
                        {
                            while (pos < item.StartB + item.insertedB)
                            {
                                sb.Append(wordArray[tm.WordCodes[pos]]);
                                sb.Append(' ');
                                pos++;
                            }
                        }
                    }

                    tm.messageHtmlText = sb.ToString();     // Previous meggage text will be garbage-collected
                }
            }
        }

        private int PrepareWordCodes(TopicMessage tm, Dictionary<string, int> word2int, List<string> int2word, int wordId)
        {
            string[] words = tm.messageHtmlText.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);    // instread "default(char[])" one can use "(char[]) null" ...
            tm.WordCodes = new int[words.Length];               // ... "If the separator parameter is null or contains no characters, white-space characters are assumed to be the delimiters"

            for (int i = 0; i < words.Length; i++)
            {
                if (word2int.ContainsKey(words[i]))
                {
                    tm.WordCodes[i] = word2int[words[i]];
                }
                else
                {
                    word2int.Add(words[i], wordId);
                    int2word.Add(words[i]);
                    tm.WordCodes[i] = wordId;
                    ++wordId;
                }
            }

            return wordId;
        }


        private string GetMessageText(int msgID)
        {
            string strSQL =  "SELECT messages.htmlText FROM messages WHERE messages.ID = " + msgID;

            DataTable msg = dbConn.GetDataTable(strSQL, dbConnection.Connenction.ListaArhiva);
            
            return msg.Rows[0]["htmlText"].ToString();
        }

    }


    /// <summary>
    /// This Class implements the Difference Algorithm published in
    /// "An O(ND) Difference Algorithm and its Variations" by Eugene Myers
    /// Algorithmica Vol. 1 No. 2, 1986, p 251.  
    /// 
    /// Some chages to the original algorithm:
    /// The original algorithm was described using a recursive approach and comparing zero indexed arrays.
    /// Extracting sub-arrays and rejoining them is very performance and memory intensive so the same
    /// (readonly) data arrays are passed arround together with their lower and upper bounds.
    /// This circumstance makes the LCS and SMS functions more complicate.
    /// I added some code to the LCS function to get a fast response on sub-arrays that are identical,
    /// completely deleted or inserted.
    /// 
    /// The result from a comparisation is stored in 2 arrays that flag for modified (deleted or inserted)
    /// lines in the 2 data arrays. These bits are then analysed to produce a array of Item objects.
    /// 
    /// Further possible optimizations:
    /// In SMS is a lot of boundary arithmetic in the for-D and for-k loops that can be done by increment
    /// and decrement of local variables.
    /// 
    /// diff.cs: A port of the algorythm to C#
    /// Copyright (c) by Matthias Hertel, http://www.mathertel.de
    /// This work is licensed under a BSD style license. See http://www.mathertel.de/License.aspx
    /// </summary>

    public class Diff
    {
        // Data structures
        private DiffData m_DataA;
        private DiffData m_DataB;

        private int[] m_DownVector;   // DownVector a vector for the (0,0) to (x,y) search
        private int[] m_UpVector;     // UpVector a vector for the (u,v) to (N,M) search



        /// <summary>details of one difference.</summary>
        public struct Item
        {
            public int StartA;    // Start Line number in Data A.
            public int StartB;    // Start Line number in Data B.

            public int deletedA;  // Number of changes in Data A.
            public int insertedB; // Number of changes in Data B.
        }



        /// <summary>
        /// Shortest Middle Snake Return Data
        /// </summary>
        private struct SMSRD
        {
            internal int x, y;
            // internal int u, v;  // 2002.09.20: no need for 2 points 
        }




        /// <summary>
        /// Find the difference in 2 arrays of integers.
        /// </summary>
        /// <param name="ArrayA">A-version of the numbers (usualy the old one)</param>
        /// <param name="ArrayB">B-version of the numbers (usualy the new one)</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        public Item[] DiffInt(int[] ArrayA, int[] ArrayB)
        {
            m_DataA = new DiffData(ArrayA);        // The A-Version of the data (original data) to be compared.
            m_DataB = new DiffData(ArrayB);        // The B-Version of the data (modified data) to be compared.

            int MAX = m_DataA.Length + m_DataB.Length + 1;
            m_DownVector = new int[2 * MAX + 2];      // vector for the (0,0) to (x,y) search
            m_UpVector = new int[2 * MAX + 2];      // vector for the (u,v) to (N,M) search

            LCS(0, m_DataA.Length, 0, m_DataB.Length);

            return CreateDiffs();
        }




        /// <summary>
        /// This is the algorithm to find the Shortest Middle Snake (SMS).
        /// </summary>
        /// <param name="LowerA">lower bound of the actual range in DataA</param>
        /// <param name="UpperA">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="LowerB">lower bound of the actual range in DataB</param>
        /// <param name="UpperB">upper bound of the actual range in DataB (exclusive)</param>
        /// <returns>a MiddleSnakeData record containing x,y and u,v</returns>
        private SMSRD SMS(int LowerA, int UpperA, int LowerB, int UpperB)
        {
            SMSRD ret;
            int MAX = m_DataA.Length + m_DataB.Length + 1;

            int DownK = LowerA - LowerB; // the k-line to start the forward search
            int UpK = UpperA - UpperB; // the k-line to start the reverse search

            int Delta = (UpperA - LowerA) - (UpperB - LowerB);
            bool oddDelta = (Delta & 1) != 0;

            // The vectors in the publication accepts negative indexes. the vectors implemented here are 0-based
            // and are access using a specific offset: UpOffset UpVector and DownOffset for DownVektor
            int DownOffset = MAX - DownK;
            int UpOffset = MAX - UpK;

            int MaxD = ((UpperA - LowerA + UpperB - LowerB) / 2) + 1;

            // init vectors
            m_DownVector[DownOffset + DownK + 1] = LowerA;
            m_UpVector[UpOffset + UpK - 1] = UpperA;

            for (int D = 0; D <= MaxD; D++)
            {
                // Extend the forward path.
                for (int k = DownK - D; k <= DownK + D; k += 2)
                {
                    // find the only or better starting point
                    int x, y;
                    if (k == DownK - D)
                    {
                        x = m_DownVector[DownOffset + k + 1]; // down
                    }
                    else
                    {
                        x = m_DownVector[DownOffset + k - 1] + 1; // a step to the right
                        if ((k < DownK + D) && (m_DownVector[DownOffset + k + 1] >= x))
                            x = m_DownVector[DownOffset + k + 1]; // down
                    }
                    y = x - k;

                    // find the end of the furthest reaching forward D-path in diagonal k.
                    while ((x < UpperA) && (y < UpperB) && (m_DataA.data[x] == m_DataB.data[y]))
                    {
                        x++; y++;
                    }
                    m_DownVector[DownOffset + k] = x;

                    // overlap ?
                    if (oddDelta && (UpK - D < k) && (k < UpK + D))
                    {
                        if (m_UpVector[UpOffset + k] <= m_DownVector[DownOffset + k])
                        {
                            ret.x = m_DownVector[DownOffset + k];
                            ret.y = m_DownVector[DownOffset + k] - k;
                            return (ret);
                        }
                    }

                } // for k

                // Extend the reverse path.
                for (int k = UpK - D; k <= UpK + D; k += 2)
                {
                    // find the only or better starting point
                    int x, y;
                    if (k == UpK + D)
                    {
                        x = m_UpVector[UpOffset + k - 1]; // up
                    }
                    else
                    {
                        x = m_UpVector[UpOffset + k + 1] - 1; // left
                        if ((k > UpK - D) && (m_UpVector[UpOffset + k - 1] < x))
                            x = m_UpVector[UpOffset + k - 1]; // up
                    } // if
                    y = x - k;

                    while ((x > LowerA) && (y > LowerB) && (m_DataA.data[x - 1] == m_DataB.data[y - 1]))
                    {
                        x--; y--; // diagonal
                    }
                    m_UpVector[UpOffset + k] = x;

                    // overlap ?
                    if (!oddDelta && (DownK - D <= k) && (k <= DownK + D))
                    {
                        if (m_UpVector[UpOffset + k] <= m_DownVector[DownOffset + k])
                        {
                            ret.x = m_DownVector[DownOffset + k];
                            ret.y = m_DownVector[DownOffset + k] - k;
                            return (ret);
                        }
                    }

                } // for k

            } // for D

            throw new ApplicationException("the algorithm should never come here.");
        } // SMS



        /// <summary>
        /// This is the divide-and-conquer implementation of the longes common-subsequence (LCS) algorithm.
        /// The published algorithm passes recursively parts of the A and B sequences.
        /// To avoid copying these arrays the lower and upper bounds are passed while the sequences stay constant.
        /// </summary>
        /// <param name="LowerA">lower bound of the actual range in DataA</param>
        /// <param name="UpperA">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="LowerB">lower bound of the actual range in DataB</param>
        /// <param name="UpperB">upper bound of the actual range in DataB (exclusive)</param>
        private void LCS(int LowerA, int UpperA, int LowerB, int UpperB)
        {
            // Fast walkthrough equal lines at the start
            while (LowerA < UpperA && LowerB < UpperB && m_DataA.data[LowerA] == m_DataB.data[LowerB])
            {
                LowerA++;
                LowerB++;
            }

            // Fast walkthrough equal lines at the end
            while (LowerA < UpperA && LowerB < UpperB && m_DataA.data[UpperA - 1] == m_DataB.data[UpperB - 1])
            {
                --UpperA;
                --UpperB;
            }

            if (LowerA == UpperA)
            {
                // mark as inserted
                while (LowerB < UpperB)
                    m_DataB.modified[LowerB++] = true;

            }
            else if (LowerB == UpperB)
            {
                // mark as deleted
                while (LowerA < UpperA)
                    m_DataA.modified[LowerA++] = true;

            }
            else
            {
                // Find the middle snakea and length of an optimal path for A and B
                SMSRD smsrd = SMS(LowerA, UpperA, LowerB, UpperB);

                // The path is from LowerX to (x,y) and (x,y) to UpperX
                LCS(LowerA, smsrd.x, LowerB, smsrd.y);
                LCS(smsrd.x, UpperA, smsrd.y, UpperB);
            }
        }





        /// <summary>Scan the tables of which lines are inserted and deleted,
        /// producing an edit script in forward order.  
        /// </summary>
        private Item[] CreateDiffs()
        {
            List<Item> a = new List<Item>();
            Item aItem;

            int StartA, StartB;
            int LineA, LineB;

            LineA = 0;
            LineB = 0;
            while (LineA < m_DataA.Length || LineB < m_DataB.Length)
            {
                if ((LineA < m_DataA.Length) && (!m_DataA.modified[LineA]) && (LineB < m_DataB.Length) && (!m_DataB.modified[LineB]))
                { // equal lines
                    LineA++;
                    LineB++;
                }
                else
                { // maybe deleted and/or inserted lines
                    StartA = LineA;
                    StartB = LineB;

                    while (LineA < m_DataA.Length && (LineB >= m_DataB.Length || m_DataA.modified[LineA]))
                        LineA++;

                    while (LineB < m_DataB.Length && (LineA >= m_DataA.Length || m_DataB.modified[LineB]))
                        LineB++;

                    if ((StartA < LineA) || (StartB < LineB))
                    { // store a new difference-item
                        aItem = new Item();
                        aItem.StartA = StartA;
                        aItem.StartB = StartB;
                        aItem.deletedA = LineA - StartA;
                        aItem.insertedB = LineB - StartB;
                        a.Add(aItem);
                    }
                }
            } // while

            return a.ToArray();
        }

    } // class Diff




    /// <summary>Data on one input file being compared.  
    /// </summary>
    internal class DiffData
    {
        /// <summary>Number of elements (lines).</summary>
        internal int Length;

        /// <summary>Buffer of numbers that will be compared.</summary>
        internal int[] data;

        /// <summary>
        /// Array of booleans that flag for modified data.
        /// This is the result of the diff.
        /// This means deletedA in the first Data or inserted in the second Data.
        /// </summary>
        internal bool[] modified;

        /// <summary>
        /// Initialize the Diff-Data buffer.
        /// </summary>
        /// <param name="data">reference to the buffer</param>
        internal DiffData(int[] initData)
        {
            data = initData;
            Length = initData.Length;
            modified = new bool[Length + 2];
        }

    } // class DiffData
}
