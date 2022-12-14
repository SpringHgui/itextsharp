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
using System.Text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.net;

namespace iTextSharp.tool.xml.pipeline.css {

    /**
     * Resolves CSS rules for a given tag.
     *
     * @author redlab_b
     *
     */
    public interface ICSSResolver {

        /**
         * This method should resolve css, meaning, it will look at the css and
         * retrieve relevant css rules for the given tag. The rules must then be set
         * in {@link Tag#setCSS(java.util.Map)}.
         *
         * @param t the tag.
         */
        void ResolveStyles(Tag t);

        /**
         * Add a piece of CSS code.
         * @param content the CSS
         * @param charSet a charset
         * @throws CssResolverException thrown if something goes wrong
         */
        void AddCss(String content, String charSet, bool isPersistent);

        /**
         * Add a
         * @param href the link to the css file ( an absolute uri )
         * @throws CssResolverException thrown if something goes wrong
         */
        void AddCssFile(String href, bool isPersistent);

        /**
         * Add a piece of CSS code.
         * @param content the content to parse to css
         * @throws CssResolverException thrown if something goes wrong
         */
        void AddCss(String content, bool isPersistent);

        /**
         * Add a CssFile
         * @param file the CssFile
         */
        void AddCss(ICssFile file);

        /**
	     * The {@link FileRetrieve} implementation to use in {@link CSSResolver#addCss(String, boolean)}.
	     * @param retrieve the retrieve to set
	     */
        IFileRetrieve FileRetrieve {set;}

        /**
         * @return
         * @throws CssResolverException
         */
        ICSSResolver Clear();

    }
}
