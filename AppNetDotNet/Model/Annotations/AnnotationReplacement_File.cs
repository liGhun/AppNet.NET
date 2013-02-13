using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class AnnotationReplacement_File : Annotation
    {
        private File file;

        public AnnotationReplacement_File(File toBeAddedFile, string type = "net.app.core.oembed", string format = "oembed")
        {
            file = toBeAddedFile;
            this.type = type;
            value = new replacement();
            value.netAppCoreFile_dummy_for_replacement.file_id = file.id;
            value.netAppCoreFile_dummy_for_replacement.file_token = file.file_token;
            value.netAppCoreFile_dummy_for_replacement.format = format;
        }

        public replacement value { get; set; }
        

    }

    public class replacement
    {
        public attachment netAppCoreFile_dummy_for_replacement { get; set; }

        public replacement()
        {
            netAppCoreFile_dummy_for_replacement = new attachment();
        }
    }

    public class attachment
    {
        public string file_id { get; set; }
        public string file_token { get; set; }
        public string format { get; set; }
    }
}
