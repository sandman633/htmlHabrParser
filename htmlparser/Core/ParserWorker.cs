using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlparser.Core
{
    class ParserWorker<T> where T: class
    {
        HtmlLoader loader;
        bool isActive;
        IParser<T> Parser { get; set; }
        IParserSettings parserSettings;
        public event Action<object, T> OnNewData;
        public event Action<object, T> OnNewDataRef;
        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }  
        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parsersetting): this(parser)
        {
            parserSettings = parsersetting;
        }

        public void Start()
        {
            isActive = true;
            Worker();
            WorkerRef();
        }
        public void Stop()
        {
            isActive = false;
        }
        private async void WorkerRef()
        {
            for (int i = parserSettings.StartPoint; i < parserSettings.EndPoint;i++ )
            {
                if(!isActive)
                {
                    return;
                }
                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);

                var result = Parser.Parse(document,Condition.Links);

                OnNewDataRef?.Invoke(this, result);
            }
            isActive = false;
        }
        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i < parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    return;
                }
                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);

                var result = Parser.Parse(document,Condition.Headers);

                OnNewData?.Invoke(this, result);
            }
            isActive = false;
        }

    }
}
