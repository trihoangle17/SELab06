using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SchoolManagementMvc.Controllers
{
    public static class SimplePdfBuilder
    {
        public static byte[] Create(IEnumerable<string> sourceLines)
        {
            var lines = sourceLines.Take(34).Select(Escape).ToList();
            var content = new StringBuilder("BT\n/F1 16 Tf\n50 790 Td\n");
            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 1) content.Append("/F1 11 Tf\n");
                content.Append('(').Append(lines[i]).Append(") Tj\n0 -21 Td\n");
            }
            content.Append("ET");
            var stream = content.ToString();
            var objects = new[]
            {
                "<< /Type /Catalog /Pages 2 0 R >>",
                "<< /Type /Pages /Kids [3 0 R] /Count 1 >>",
                "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>",
                "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>",
                "<< /Length " + Encoding.ASCII.GetByteCount(stream) + " >>\nstream\n" + stream + "\nendstream"
            };
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms, Encoding.ASCII, 1024, true) { NewLine = "\n" })
            {
                writer.WriteLine("%PDF-1.4"); writer.Flush();
                var offsets = new List<long> { 0 };
                for (int i = 0; i < objects.Length; i++)
                {
                    offsets.Add(ms.Position); writer.WriteLine((i + 1) + " 0 obj"); writer.WriteLine(objects[i]); writer.WriteLine("endobj"); writer.Flush();
                }
                long xref = ms.Position;
                writer.WriteLine("xref"); writer.WriteLine("0 " + (objects.Length + 1)); writer.WriteLine("0000000000 65535 f ");
                foreach (var offset in offsets.Skip(1)) writer.WriteLine(offset.ToString("0000000000") + " 00000 n ");
                writer.WriteLine("trailer"); writer.WriteLine("<< /Size " + (objects.Length + 1) + " /Root 1 0 R >>"); writer.WriteLine("startxref"); writer.WriteLine(xref); writer.WriteLine("%%EOF"); writer.Flush();
                return ms.ToArray();
            }
        }
        private static string Escape(string value) { return (value ?? "").Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)"); }
    }
}
