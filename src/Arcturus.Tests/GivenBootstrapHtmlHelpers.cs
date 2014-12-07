using Arcturus.Extensions;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Arcturus.Tests
{
    public class GivenBootstrapHtmlHelpers : ViewContext, IViewDataContainer
    {
        [Test]
        public void WhenICallEditorForModel_TheOutputIsCorrect()
        {
            const string expectedOutcome = @"<div class=""form-group""><label for=""PropertyA"">PropertyA</label><input type=""text"" class=""form-control"" id=""propertyA"" placeholder=""Property A""></div>""";
            var helper = CreateHtmlHelper<TestClass>(new ViewDataDictionary());
            var result =  helper.EditorFor(t => t.MyString, "My String", "mystring");
        }

        private static HtmlHelper<TModel> CreateHtmlHelper<TModel>(ViewDataDictionary vd)
        {
            Mock<ViewContext> mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object
                ),
                new Mock<IView>().Object,
                vd,
                new TempDataDictionary(),
                new StreamWriter(new MemoryStream())
            );

            ViewContext vc = new ViewContext();

            Mock<IViewDataContainer> mockDataContainer = new Mock<IViewDataContainer>();
            mockDataContainer.Setup(c => c.ViewData).Returns(vd);

            return new HtmlHelper<TModel>(mockViewContext.Object, mockDataContainer.Object);
        }
    }
}
