/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2022 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.
    
    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com
 */
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace itextsharp.xmlworker.tests.iTextSharp.tool.xml.examples.css.div {
    public class ComplexDiv01Test : SampleTest {
        protected override string GetTestName() {
            return "complexDiv01";
        }

        protected override void MakePdf(string outPdf) {
            Document doc = new Document(PageSize.A3.Rotate());

            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(outPdf, FileMode.Create));
            pdfWriter.CreateXmpMetadata();

            doc.SetMargins(200, 200, 0, 0);
            doc.Open();


            CssFilesImpl cssFiles = new CssFilesImpl();
            cssFiles.Add(
                XMLWorkerHelper.GetCSS(
                    File.OpenRead(RESOURCES + Path.DirectorySeparatorChar + testPath + Path.DirectorySeparatorChar + testName +
                                  Path.DirectorySeparatorChar + "complexDiv_files" + Path.DirectorySeparatorChar +
                                  "main.css")));
            cssFiles.Add(
                XMLWorkerHelper.GetCSS(
                    File.OpenRead(RESOURCES + Path.DirectorySeparatorChar + testPath + Path.DirectorySeparatorChar + testName +
                                  Path.DirectorySeparatorChar + "complexDiv_files" + Path.DirectorySeparatorChar +
                                  "widget082.css")));
            StyleAttrCSSResolver cssResolver = new StyleAttrCSSResolver(cssFiles);
            HtmlPipelineContext hpc =
                new HtmlPipelineContext(
                    new CssAppliersImpl(new XMLWorkerFontProvider(RESOURCES + @"\tool\xml\examples\fonts")));
            hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(Tags.GetHtmlTagProcessorFactory());
            hpc.SetImageProvider(new SampleTestImageProvider());
            hpc.SetPageSize(doc.PageSize);
            HtmlPipeline htmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(doc, pdfWriter));
            IPipeline pipeline = new CssResolverPipeline(cssResolver, htmlPipeline);
            XMLWorker worker = new XMLWorker(pipeline, true);
            XMLParser p = new XMLParser(true, worker, Encoding.GetEncoding("UTF-8"));
            p.Parse(File.OpenRead(inputHtml), Encoding.GetEncoding("UTF-8"));
            //ICC_Profile icc = ICC_Profile.getInstance(ComplexDiv01Test.class.getResourceAsStream("sRGB Color Space Profile.icm"));
            //pdfWriter.setOutputIntents("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", icc);
            doc.Close();
        }
    }
}
