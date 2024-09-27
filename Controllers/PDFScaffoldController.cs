using Microsoft.AspNetCore.Mvc;
using MigraDoc.DocumentObjectModel;
using PDFScaffold.Images;
using PDFScaffold.Layout;
using PDFScaffold.Metrics;
using PDFScaffold.Scaffold;
using PDFScaffold.Styling;
using PDFScaffold.Tables;
using PDFScaffold.Texts;

namespace PDFScaffoldExamples.Controllers
{
    [Route("api/scaffold")]
    [ApiController]
    public class PDFScaffoldController : Controller
    {

        [HttpGet]
        public IActionResult GetDocument()
        {
            var lorem =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam venenatis mattis condimentum. Sed egestas aliquam diam, at tristique sem lacinia quis. Proin rutrum, ipsum eget feugiat condimentum, nunc dui viverra nibh, nec malesuada mauris neque quis arcu. Nunc porttitor risus urna, at convallis nunc posuere vitae. Curabitur bibendum lorem vitae risus rutrum tempus. Sed sed risus ut quam tempus blandit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In hac habitasse platea dictumst. Ut quis iaculis tellus, sit amet dignissim enim. Suspendisse vel scelerisque lectus.";
            var miniLorem =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam venenatis mattis condimentum. Sed egestas aliquam diam, at tristique sem lacinia quis. Proin rutrum, ipsum eget feugiat condimentum, nunc dui viverra nibh, nec malesuada mauris neque quis arcu.";
            var fontStyle = new SStyle(
                fontSize: new SMeasure(pixels: 14.5),
                alignment: SAlignment.Justified);
            var firstImage = new SImage(
                name: "firstImage",
                path: Environment.CurrentDirectory + "/Assets/cover-img.png",
                style: new SStyle(width: new SMeasure(percentage: 4.0 / 7.0)));
            var image = new SImage(
                path: Environment.CurrentDirectory + "/Assets/cover-img.png",
                style: new SStyle(width: new SMeasure(percentage: 4.0 / 7.0)));
            var imageTwo = new SImage(
                path: Environment.CurrentDirectory + "/Assets/cover-img.png",
                style: new SStyle(width: new SMeasure(percentage: 1 / 3.0)));

            var doc = new SDocument(sections: [
                new(elements: [
                new SHeading("Exemplo de utilização da biblioteca PDFScaffold.", style: new SStyle(horizontalAlignment: SAlignment.Justified)),
                new SRow(elements: [
                    new SContainer(
                        new SParagraph(content: lorem, style: fontStyle),
                        style: new SStyle(
                            padding: new SPadding(
                                right: new SMeasure(percentage: 0.1)),
                            width: new SMeasure(percentage: 3.0/7.0))),
                    firstImage,
                ]),
                new SParagraph(lorem, style: fontStyle),
                new SContainer(style: new SStyle(height: new SMeasure(pixels: 10))),
                new SRow(elements: [
                    image,
                    new SContainer(
                        content: new SParagraph(content: lorem, style: fontStyle),
                        style: new SStyle(
                            padding: new SPadding(
                                left: new SMeasure(percentage: 0.1)),
                            width: new SMeasure(percentage: 3.0/7.0))),
                ]),
                new SParagraph([
                    new SText(lorem, style: fontStyle.Merge(new SStyle(fontColor: Colors.Brown))),
                    new SText(lorem, style: fontStyle.Merge(new SStyle(fontColor: Colors.Purple))),
                ], fontStyle),
                new SContainer(style: new SStyle(height: new SMeasure(pixels: 10))),
                new STable([
                    new STableRow([
                        new STableCell(imageTwo),
                        new STableCell(
                            new SContainer(
                                new SParagraph(miniLorem, style: fontStyle),
                                style: new SStyle(
                                    padding: new SPadding(leftAndRight: new(percentage: 0.1)),
                                    width: new SMeasure(percentage: 1/3.0),
                                    height: new SMeasure(percentage: 0.25)))),
                        new STableCell(imageTwo),
                    ], style: new SStyle(verticalAlignment: SAlignment.Center))
                ]),
                new SHeading("Using references", level: 2, style: new SStyle(verticalAlignment: SAlignment.Center)),
                new SRow(elements: [
                    new SParagraph("In this paragraph, it is being used the reference of a style defined inside the SDocument. This makes possible to share styles even when the component is being written in another class or file. Using this reference, this paragraph was made to have a font size of 16 pixels, and a blue color.", useStyle: "paragraphReference"),
                    new SParagraph(content: [
                        new SText("In this paragraph, it will be demonstrated the use of a reference to another component inside the SDocument. "),
                        new SLink("#firstImage", " This link takes the user to the first image in the SDocument."),
                        new SText(" The first image in the SDocument was named "),
                        new SText("firstImage", useStyle: "bookmark"),
                        new SText(", which made possible to reference using an SLink with the a link "),
                        new SText("#firstImage", useStyle: "bookmark"),
                        new SText(". Furthermore, in this paragraph was used several references to an SStyle defined inside the SDocument.")
                    ], useStyle: "lastParagraph")
                ])
            ])], styles: [
                new SStyle("paragraphReference",
                    fontSize: new SMeasure(pixels: 16),
                    fontColor: Colors.DarkBlue,
                    alignment: SAlignment.Justified),
                new SStyle("lastParagraph", fontSize: new SMeasure(pixels: 16), alignment: SAlignment.Justified),
                new SStyle("bookmark", fontColor: Colors.DarkRed)
            ]);

            return File(doc.GeneratePdf(), "application/pdf");
        }

    }
}