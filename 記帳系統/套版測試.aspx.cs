using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace 記帳系統
{
    public partial class 套版測試 : System.Web.UI.Page
    {
        // 定義結果資料夾的變數，請替換為實際路徑
        private static readonly string ResultFolder = @"C:\Users\User\Desktop\報表";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            // 呼叫 Word 範本渲染方法
            Example01_WordTmplRendering();
        }

        static void Example01_WordTmplRendering()
        {/*
            var templatePath = @"C:\Users\User\Desktop\資料庫\記帳系統\記帳系統\套板模板測試.docx";
            var docxBytes = Examples.WordRender.GenerateDocx(File.ReadAllBytes(templatePath),
                new Dictionary<string, string>()
                {
                    ["Title"] = "澄清黑暗執行緒部落格併購傳聞",
                    ["book"] = "澄清dododo"
                });
            File.WriteAllBytes(
                Path.Combine(ResultFolder, $"套表測試-{DateTime.Now:HHmmss}.docx"),
                docxBytes);*/
        }
    }

    /*namespace Examples
    {
        public static class WordRender
        {
            static void ReplaceParserTag(this OpenXmlElement elem, Dictionary<string, string> data)
            {
                var pool = new List<Run>();
                var matchText = string.Empty;
                var hiliteRuns = elem.Descendants<Run>() //找出鮮明提示
                    .Where(o => o.RunProperties?.Elements<Highlight>().Any() ?? false).ToList();

                foreach (var run in hiliteRuns)
                {
                    var t = run.InnerText;
                    if (t.StartsWith("["))
                    {
                        pool = new List<Run> { run };
                        matchText = t;
                    }
                    else
                    {
                        matchText += t;
                        pool.Add(run);
                    }
                    if (t.EndsWith("]"))
                    {
                        var m = Regex.Match(matchText, @"\[\$(?<n>\w+)\$\]");
                        if (m.Success && data.ContainsKey(m.Groups["n"].Value))
                        {
                            var firstRun = pool.First();
                            firstRun.RemoveAllChildren<Text>();
                            firstRun.RunProperties.RemoveAllChildren<Highlight>();
                            var newText = data[m.Groups["n"].Value];
                            var firstLine = true;
                            foreach (var line in Regex.Split(newText, @"\\n"))
                            {
                                if (firstLine) firstLine = false;
                                else firstRun.Append(new Break());
                                firstRun.Append(new Text(line));
                            }
                            pool.Skip(1).ToList().ForEach(o => o.Remove());
                        }
                    }

                }
            }

            public static byte[] GenerateDocx(byte[] template, Dictionary<string, string> data)
            {
                using (var ms = new MemoryStream())
                {
                    ms.Write(template, 0, template.Length);
                    using (var docx = WordprocessingDocument.Open(ms, true))
                    {
                        docx.MainDocumentPart.HeaderParts.ToList().ForEach(hdr =>
                        {
                            hdr.Header.ReplaceParserTag(data);
                        });
                        docx.MainDocumentPart.FooterParts.ToList().ForEach(ftr =>
                        {
                            ftr.Footer.ReplaceParserTag(data);
                        });
                        docx.MainDocumentPart.Document.Body.ReplaceParserTag(data);
                        docx.Save();
                    }
                    return ms.ToArray();
                }
            }
        }
    }*/

}