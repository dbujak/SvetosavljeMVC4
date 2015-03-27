using System;
using System.Collections.Generic;

namespace Svetosavlje.Interfaces.Classes
{
    public class MessageThread
    {
        public int id;
        public string naziv;
        public long count;
        public string updater;
        public int updaterID;
        public DateTime? datum;

        public MessageThread(int i, string n, long c, string u, DateTime? d, int updId)
        {
            id = i;
            naziv = n;
            count = c;
            updater = u;
            datum = d;
            updaterID = updId;
        }

        public MessageThread() { }
    }

    public class AttachmentInfo
    {
        public int id;
        public string title;
        public string type;
        public long size;

        public AttachmentInfo(int i, string ttl, string typ, long sz)
        {
            id = i;
            title = ttl;
            type = typ;
            size = sz;
        }

        public AttachmentInfo() { }
    }

    public class TopicMessage
    {
        public int messageID;
        public int userID;
        public string userName;
        public DateTime messageDate;
        public string messageHtmlText;
        public IList<AttachmentInfo> attach;
        public int inReplyTo;
        public string emailHeader;
        public int[] WordCodes;
        public TopicMessage InReplyTo;

        public TopicMessage(int id, string htmlText)
        {
            messageID = id;
            userID = 0;
            userName = null;
            messageDate = DateTime.Now;
            messageHtmlText = htmlText;
            attach = null;
            inReplyTo = 0;
            emailHeader = null;

            WordCodes = null;
            InReplyTo = null;
        }

        public TopicMessage(int id, int uid, string name, DateTime time, string htmlText, List<AttachmentInfo> att, int inReply, string hdr)
        {
            messageID = id;
            userID = uid;
            userName = name;
            messageDate = time;
            messageHtmlText = htmlText;
            attach = att;
            inReplyTo = inReply;
            emailHeader = hdr;

            WordCodes = null;
            InReplyTo = null;
        }

        public TopicMessage() { }
    }
}
