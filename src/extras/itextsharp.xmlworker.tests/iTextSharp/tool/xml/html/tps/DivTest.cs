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
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.html.pdfelement;
using iTextSharp.tool.xml.pipeline.ctx;
using iTextSharp.tool.xml.pipeline.html;
using NUnit.Framework;

namespace itextsharp.xmlworker.tests.iTextSharp.tool.xml.html.tps {
    /**
 * @author itextpdf.com
 *
 */

    internal class DivTest {
        private Div d = new Div();
        private List<IElement> currentContent = new List<IElement>();
        private WorkerContextImpl workerContextImpl;

        [SetUp]
        virtual public void SetUp() {
            workerContextImpl = new WorkerContextImpl();
            workerContextImpl.Put(typeof (HtmlPipeline).FullName, new HtmlPipelineContext(null));
            currentContent.Add(new Paragraph("titel paragraph"));
            currentContent.Add(Chunk.NEWLINE);
            currentContent.Add(new NoNewLineParagraph("first content text"));
            currentContent.Add(new Paragraph("footer text"));
            d.SetCssAppliers(new CssAppliersImpl());
        }

        /**
	 * Verifies that the call to content of {@link Div} returns a NoNewLineParagraph.
	 */

        [Test]
        virtual public void VerifyContent() {
            IList<IElement> content = d.Content(workerContextImpl, new Tag("div"), "text inside a div tag");
            Assert.IsTrue(content[0] is NoNewLineParagraph);
        }

        /**
	 * Verifies that the numbers of paragraphs returned by {@link Div#end}.
	 */

        [Test]
        virtual public void VerifyNumberOfParagraphs() {
            IList<IElement> endContent = d.End(workerContextImpl, new Tag("div"), currentContent);
            Assert.AreEqual(1, endContent.Count);
        }

        /**
	 * Verifies that the class of the elements returned by {@link Div#end}.
	 */

        [Test]
        virtual public void VerifyIfPdfDiv() {
            IList<IElement> endContent = d.End(workerContextImpl, new Tag("div"), currentContent);
            Assert.IsTrue(endContent[0] is PdfDiv);
        }

        /**
	 * Verifies if {@link Div} is a stack owner. Should be true.
	 */

        [Test]
        virtual public void VerifyIfStackOwner() {
            Assert.IsTrue(d.IsStackOwner());
        }
    }
}
