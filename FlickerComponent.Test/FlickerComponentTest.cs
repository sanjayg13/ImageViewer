using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FlickerComponent.Interfaces;

namespace FlickerComponent.Test
{
    /// <summary>
    ///     Test class Flicker component.
    /// </summary>
    [TestFixture]
    public class FlickerComponetTest
    {
        private IResponseHandler _flickerApiResponseHandler;

        [SetUp]
        public void Setup()
        {
            _flickerApiResponseHandler = new FlickerApiResponseHandler();
            FlickerApiResponseHandler.BaseUrl = "https://api.flickr.com/services/rest/?api_key=ba317ab06ec79489ee49c798adc9997f&";
        }

        [Test]
        public void TestFlickerApiNotNullOrEmpty()
        {
            var response = _flickerApiResponseHandler.GetResponse("method=flickr.photos.search&text=cats");
            Assert.NotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void TestFlickerApiReturnGoodResponse()
        {
            var response = _flickerApiResponseHandler.GetResponse("method=flickr.photos.search&text=cats");
            var status = GetStatusAttribute(response);
            Assert.AreEqual(status,"ok");
        }

        [Test]

        public void TestFlickerApiShouldThrowException()
        {
            FlickerApiResponseHandler.BaseUrl = "https://api.flickr.com/services/rest/api_key=ba317ab06ec79489ee49c798adc9997f&";
            Assert.Throws<WebException>(() => _flickerApiResponseHandler.GetResponse(""));
        }

        private static string GetStatusAttribute(string response)
        {
            var reader = XmlReader.Create(new StringReader(response), new XmlReaderSettings
            {
                IgnoreWhitespace = true
            });
            reader.ReadToDescendant("rsp");
            while (reader.MoveToNextAttribute())
            {
                if (reader.LocalName == "stat")
                {
                    return reader.Value;
                }
            }
            return String.Empty;
            ;
        }
    }
}
