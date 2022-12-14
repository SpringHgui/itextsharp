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
using iTextSharp.tool.xml.html;

namespace iTextSharp.tool.xml.util {

/**
 * Util class for everything related to a ParentTree of a Tag
 * 
 * 
 * @author Jeroen Nouws
 *
 */

    public class ParentTreeUtil
    {
        /**
         * creates a parentTree of all parents of a given Tag
         * 
         * @param t Tag the tag where you want to know it's parentTree of
         * @return parentTree a arrayList<String> containing the parents of Tag t
         */
        public IList<String> GetParentTree(Tag t){
            IList<String> parentTree = new List<String>();
            Tag parent = t.Parent;
            while(parent!=null&&!(parent.Name.Equals(HTML.Tag.BODY))){
                parentTree.Add(parent.Name);
                parent = parent.Parent;
            }
            return parentTree;
        }

        /**
         * creates a parentTree of all parent Tag objects of a given Tag
         * 
         * @param tag Tag the tag where you want to know it's parentTree of
         * @param tree
         * @return parentTree arrayList<Tag> containing the parent tags of Tag t
         */
        public IList<Tag> GetParentTagTree(Tag tag, IList<Tag> tree) {
            IList<Tag> parentTree = new List<Tag>();
            Tag parent = tag.Parent;
            while (parent != null && !(parent.Name.Equals(HTML.Tag.HTML))) {
                parentTree.Add(parent);
                parent = parent.Parent;
            }
            return parentTree;
        }
    }
}
