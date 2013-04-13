using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Data_Layer.Interfaces
{
    public interface IDataProvider : ITopicList, IQuestionList, IGetList, IQuote, IZachala, IFastingType, IPastirTopicsList
    {

    }

    public interface ITopicList
    {
        IList<MailListTopicInfo> GetTopicList(int rows);
    }

    public interface IQuestionList
    {
        IList<PitanjeInfo> GetQuestionList(int rows);
    }

    public interface IPastirTopicsList
    {
        IList<PastirTopic> GetPastirTopicList();
    }

    public interface IGetList
    {
        IList<string> GetList(int Mjesec, int Dan);
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
