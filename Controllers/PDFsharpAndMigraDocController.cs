using Microsoft.AspNetCore.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace PDFScaffoldExamples.Controllers
{

    [Route("api/sharp")]
    [ApiController]
    public class PDFsharpAndMigraDocController : Controller
    {

        [HttpGet]
        public IActionResult GetDocument()
        {
            var miniLorem =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam venenatis mattis condimentum. Sed egestas aliquam diam, at tristique sem lacinia quis. Proin rutrum, ipsum eget feugiat condimentum, nunc dui viverra nibh, nec malesuada mauris neque quis arcu.";
            
            var lorem =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam venenatis mattis condimentum. Sed egestas aliquam diam, at tristique sem lacinia quis. Proin rutrum, ipsum eget feugiat condimentum, nunc dui viverra nibh, nec malesuada mauris neque quis arcu. Nunc porttitor risus urna, at convallis nunc posuere vitae. Curabitur bibendum lorem vitae risus rutrum tempus. Sed sed risus ut quam tempus blandit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. In hac habitasse platea dictumst. Ut quis iaculis tellus, sit amet dignissim enim. Suspendisse vel scelerisque lectus.";

            var document = new Document();

            var h1Style = document.Styles[StyleNames.Heading1]!;
            var h2Style = document.Styles[StyleNames.Heading2]!;
            var fontStyle = document.AddStyle("fontStyle", StyleNames.Normal);

            h1Style.Font.Size = Unit.FromPoint(25.5);
            h2Style.Font.Size = Unit.FromPoint(22.5);
            h1Style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            h2Style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            fontStyle.Font.Size = Unit.FromPoint(10.875);
            fontStyle.ParagraphFormat.SpaceBefore = Unit.FromPoint(10);
            fontStyle.ParagraphFormat.Alignment = ParagraphAlignment.Justify;

            var section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;

            var h1 = section.AddParagraph("Exemplo de utilização da biblioteca PDFsharp & MigraDoc.", StyleNames.Heading1);

            var firstTable = section.AddTable();
            firstTable.AddColumn(Unit.FromInch(3 * 6.3 / 7.0));
            firstTable.AddColumn(Unit.FromInch(4 * 6.3 / 7.0));

            var row = firstTable.AddRow();

            var firstParagraph = row.Cells[0].AddParagraph(lorem);
            firstParagraph.Style = "fontStyle";
            firstParagraph.Format.RightIndent = Unit.FromInch(3 * 6.3 / 70);

            var bookmarkForfirstImage = row.Cells[1].AddTextFrame();
            bookmarkForfirstImage.Width = 0;
            bookmarkForfirstImage.Height = 0;
            bookmarkForfirstImage.AddParagraph().AddBookmark("firstImage");
            var firstImage = row.Cells[1].AddImage(Environment.CurrentDirectory + "/Assets/cover-img.png");
            firstImage.Width = Unit.FromInch(4 * 6.3 / 7.0);

            section.AddParagraph(lorem, "fontStyle").Format.SpaceAfter = 7.5;

            var secondTable = section.AddTable();
            secondTable.AddColumn(Unit.FromInch(4 * 6.3 / 7.0));
            secondTable.AddColumn(Unit.FromInch(3 * 6.3 / 7.0));

            row = secondTable.AddRow();

            var secondImage = row.Cells[0].AddImage(Environment.CurrentDirectory + "/Assets/cover-img.png");
            secondImage.Width = Unit.FromInch(4 * 6.3 / 7.0);

            var thirdParagraph = row.Cells[1].AddParagraph(lorem);
            thirdParagraph.Style = "fontStyle";
            thirdParagraph.Format.LeftIndent = Unit.FromInch(3 * 6.3 / 70);

            var fourthParagraph = section.AddParagraph("", style: "fontStyle");
            fourthParagraph.AddFormattedText(lorem).Font.Color = Colors.Brown;
            fourthParagraph.AddFormattedText(lorem).Font.Color = Colors.Purple;
            fourthParagraph.Format.SpaceAfter = 7.5;

            var thirdTable = section.AddTable();
            thirdTable.AddColumn(Unit.FromInch(6.3 / 3));
            thirdTable.AddColumn(Unit.FromInch(6.3 / 3));
            thirdTable.AddColumn(Unit.FromInch(6.3 / 3));

            row = thirdTable.AddRow();

            var image = row.Cells[0].AddImage(Environment.CurrentDirectory + "/Assets/cover-img.png");
            image.Width = Unit.FromInch(6.3 / 3);
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

            image = row.Cells[2].AddImage(Environment.CurrentDirectory + "/Assets/cover-img.png");
            image.Width = Unit.FromInch(6.3 / 3);
            row.Cells[2].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

            var paragraph = row.Cells[1].AddParagraph(miniLorem);
            paragraph.Style = "fontStyle";
            paragraph.Format.LeftIndent = Unit.FromInch(6.3 / 30);
            paragraph.Format.RightIndent = Unit.FromInch(6.3 / 30);

            var h2 = section.AddParagraph("Using Reference", StyleNames.Heading2);

            var fourthTable = section.AddTable();
            fourthTable.AddColumn(Unit.FromInch(6.3 / 2));
            fourthTable.AddColumn(Unit.FromInch(6.3 / 2));

            row = fourthTable.AddRow();

            paragraph = row.Cells[0].AddParagraph("In this paragraph, it is being used the reference of a style defined inside the SDocument. This makes possible to share styles even when the component is being written in another class or file. Using this reference, this paragraph was made to have a font size of 16 pixels, and a blue color.");
            paragraph.Style = "fontStyle";
            paragraph.Format.Font.Size = Unit.FromPoint(12);
            paragraph.Format.Font.Color = Colors.DarkBlue;

            paragraph = row.Cells[1].AddParagraph();
            paragraph.Style = "fontStyle";
            paragraph.Format.Font.Size = Unit.FromPoint(12);
            paragraph.AddFormattedText("In this paragraph, it will be demonstrated the use of a reference to another component inside the SDocument. ");
            paragraph.AddHyperlink("firstImage").AddFormattedText(" This link takes the user to the first image in the SDocument.").Font.Color = Colors.Purple;
            paragraph.AddFormattedText(" The first image in the SDocument was named ");
            paragraph.AddFormattedText("firstImage").Font.Color = Colors.DarkRed;
            paragraph.AddFormattedText(", which made possible to reference using an SLink with the a link ");
            paragraph.AddFormattedText("#firstImage").Font.Color = Colors.DarkRed;
            paragraph.AddFormattedText(". Furthermore, in this paragraph was used several references to an SStyle defined inside the SDocument.");

            PdfDocumentRenderer pdfDocumentRenderer = new()
            {
                Document = document
            };
            pdfDocumentRenderer.PdfDocument.PageLayout = PdfPageLayout.SinglePage;
            pdfDocumentRenderer.PdfDocument.ViewerPreferences.FitWindow = true;
            pdfDocumentRenderer.RenderDocument();
            MemoryStream memoryStream = new();
            pdfDocumentRenderer.PdfDocument.Save(memoryStream);
            byte[] result = memoryStream.ToArray();
            memoryStream.Close();

            return File(result, "application/pdf");
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult HelloWorld()
        {
            var document = new Document();
            var section = document.AddSection();
            section.AddParagraph("Hello, World!");

            var renderer = new PdfDocumentRenderer {
              Document = document,
              PdfDocument = {
                PageLayout = PdfPageLayout.SinglePage,
                ViewerPreferences = {
                    FitWindow = true
                }
              }
            };

            renderer.RenderDocument();
            var ms = new MemoryStream();
            renderer.PdfDocument.Save(ms);
            byte[] result = ms.ToArray();
            ms.Close();

            return File(result, "application/pdf");
        }

    }

}