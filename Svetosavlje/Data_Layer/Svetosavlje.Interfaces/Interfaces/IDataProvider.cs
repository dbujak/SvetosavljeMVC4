using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Classes;

namespace Svetosavlje.Interfaces.Interfaces
{
    public interface IDataProvider : IListaArhiva, IQuestionList, ISaintNamesList, ISaintNamesAndLivesList, IProlog, IQuote, IZachala, IFastingType, IPastirTopicsList, IPastirQuestion
    {

    }

    public interface IListaArhiva
    {
        IList<MessageThread> GetMessageThreads(int rows);
        IList<MessageThread> GetMessageThreads(int page, int rows);
        IList<TopicMessage> GetTopicMessages(int topicID);
        int GetTotalTopics();
    }


    public interface IPastirQuestion
    {
        PitanjeInfo GetPastirQuestion(int questionID);
    }

    public interface IQuestionList
    {
        IList<PitanjeInfo> GetQuestionList(int topicID, int rows);
    }

    public interface IPastirTopicsList
    {
        IList<PastirTopic> GetPastirTopicList();
    }

    public interface ISaintNamesList
    {
        IList<string> GetSaintNamesList(int Mjesec, int Dan);
    }

    public interface ISaintNamesAndLivesList
    {
        IList<Prolog> GetSaintNamesAndLivesList(int Mjesec, int Dan);
    }

    public interface IProlog
    {
        PrologOther GetProlog(int Mjesec, int Dan);
    }

    public interface IQuote
    {
        string GetQuote(int Autor, int Mjesec, int Dan);
    }

    public interface IZachala
    {
        string GetZachala(int Mjesec, int Dan, int Godina);
    }

    public interface IFastingType
    {
        string GetFastingType(int Mjesec, int Dan);
    }

}
