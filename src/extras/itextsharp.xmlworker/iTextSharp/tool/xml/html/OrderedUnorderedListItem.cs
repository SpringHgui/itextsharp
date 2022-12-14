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
using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.exceptions;
namespace iTextSharp.tool.xml.html {

    /**
     * @author redlab_b
     *
     */
    public class OrderedUnorderedListItem : AbstractTagProcessor {

        /* (non-Javadoc)
         * @see com.itextpdf.tool.xml.ITagProcessor#content(com.itextpdf.tool.xml.Tag, java.lang.String)
         */
        public override IList<IElement> Content(IWorkerContext ctx, Tag tag, String content) {
            return TextContent(ctx, tag, content);
        }

        /* (non-Javadoc)
         * @see com.itextpdf.tool.xml.ITagProcessor#endElement(com.itextpdf.tool.xml.Tag, java.util.List)
         */
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent) {
            IList<IElement> l = new List<IElement>(1);
            ListItem li = new ListItem();
            float maxSize = -1;
            foreach (IElement e in currentContent) {
                li.Add(e);
                //finding max leading among list item elements
                foreach (Chunk chunk in e.Chunks) {
                    // here we use 4f/3 multiplied leading value to simulate leading which is used with default font size
                    float currFontSize = chunk.Font.GetCalculatedLeading(4f/3);
                    if (maxSize < currFontSize) {
                        maxSize = currFontSize;
                    }
                }
            }
            if (li.Leading < maxSize)
                li.Leading = maxSize;
            if (li.Trim()) {
                l.Add(li); // css applying is handled in the OrderedUnorderedList Class.
            }
            return l;
        }

        /* (non-Javadoc)
         * @see com.itextpdf.tool.xml.ITagProcessor#isStackOwner()
         */
        public override bool IsStackOwner() {
            return true;
        }
    }
}
