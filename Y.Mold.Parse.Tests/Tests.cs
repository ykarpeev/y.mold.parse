using System.Xml;

namespace Y.Mold.Parse.Tests
{
    public class Tests
    {
        private string Sample1 { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Sample1 = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?>\n\n" +
                           "<svg viewBox=\"-100 -100 100 100\" xmlns=\"http://www.w3.org/2000/svg\">\n\n" +
                           "<line id=\"MarkingTag\" x1=\"25.802763\" y1=\"-23.191372\" x2=\"17.987909\" " +
                           "y2=\"-3.758350\" text=\"Y1\" stroke=\"green\" height_start=\"16.216911\" " +
                           "height_end=\"16.238251\" height_avg=\"15.913533\"/>\n\n" +
                           "<line id=\"GraphicMarkingTag\" x1=\"-21.505285\" y1=\"-11.948075\" x2=\"-20.012747\" " +
                           "y2=\"-8.237306\" width=\"4\" height=\"4\" stroke=\"green\" height_start=\"15.155739\" " +
                           "height_end=\"15.436935\" height_avg=\"15.378740\"/>\n\n</svg>";
        }

        [Test]
        public void BasicTest()
        {
            var d = new MoldingParser().Parse(this.Sample1);
            Assert.That(d.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(d[0].ID, Is.EqualTo("MarkingTag"));
                Assert.That(d[0].X1, Is.EqualTo(25.802763));
                Assert.That(d[0].X2, Is.EqualTo(17.987909));
                Assert.That(d[0].Y1, Is.EqualTo(-23.191372));
                Assert.That(d[0].Y2, Is.EqualTo(-3.758350));
                Assert.That(d[0].Text, Is.EqualTo("Y1"));
                Assert.That(d[0].Stroke, Is.EqualTo("green"));
                Assert.That(d[0].Height_Start, Is.EqualTo(16.216911));
                Assert.That(d[0].Height_End, Is.EqualTo(16.238251));
                Assert.That(d[0].Height_Avg, Is.EqualTo(15.913533));
                Assert.That(d[0].IsText, Is.True);
            });
        }

        [Test]
        public void BadXMLFileTest()
        {
            Assert.Throws<XmlException>(() => new MoldingParser().Parse("XYZ"));
        }
    }
}