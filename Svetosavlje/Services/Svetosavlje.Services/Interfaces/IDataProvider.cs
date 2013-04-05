using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Services.Interfaces
{
    public interface IDataProvider
    {
        IList<MailListTopicInfo> TopicList(int rows);
        IList<PitanjeInfo> QuestionList(int rows);
        IList<string> GetList(int Mjesec, int Dan);
        string GetQuote(int Autor, int Mjesec, int Dan);
        string GetFastingType(int Mjesec, int Dan);
        string GetZachala(int Mjesec, int Dan, int Godina);
        IList<WPBlogModel> GetNews(int rows);

    }
}
