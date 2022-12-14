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
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.html;
namespace iTextSharp.tool.xml.parser.state {

    /**
     * @author redlab_b
     *
     */
    public class TagAttributeState : IState {

        protected XMLParser parser;

        /**
         * @param parser the XMLParser
         */
        public TagAttributeState(XMLParser parser) {
            this.parser = parser;
        }

        /* (non-Javadoc)
         * @see com.itextpdf.tool.xml.parser.State#process(int)
         */
        public virtual void Process(char character) {
            if (character == '/') {
                // self closing tag detected
                parser.SelectState().SelfClosing();
            } else if (character == '=') {
                this.parser.Memory().CurrentAttr(this.parser.BufferToString());
                this.parser.Flush();
                this.parser.SelectState().AttributeValue();
            } else if (HTMLUtils.IsWhiteSpace(character)){
                SetAttribute();
            } else if (character == '>') {
                CheckAttributeWithNoValue();
                this.parser.StartElement();
                this.parser.Flush();
                this.parser.SelectState().InTag();
            } else if (this.parser.Memory().HasCurrentAttribute() && !HTMLUtils.IsWhiteSpace(character)) {
                // assume attribute with no value, example tag <?formServer defaultPDFRenderFormat acrobat8.1dynamic?>
                this.parser.Memory().PutCurrentAttrValue("");
                this.parser.Append(character);
            } else {
                this.parser.Append(character);
            }
        }

        virtual protected void CheckAttributeWithNoValue() {
            if (SetAttribute()) {
                this.parser.Memory().PutCurrentAttrValue("");
            }
        }

        /**
         * @return true if an attribute was set
         *
         */
        virtual protected internal bool SetAttribute() {
            String attr = this.parser.BufferToString();
            if (attr.Length > 0) {
                this.parser.Memory().CurrentAttr(attr);
                this.parser.Flush();
                return true;
            }
            return false;
        }
    }
}
