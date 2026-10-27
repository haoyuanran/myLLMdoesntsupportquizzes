// JSON Quiz desktop launcher.
// Carries index.html as an embedded resource, writes it to %LOCALAPPDATA%\JsonQuiz and opens it
// in an Edge (or Chrome) "app" window. If a .json file is passed (drag it onto the exe or use
// "Open with"), the quiz is injected into the page so it opens straight away.
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        try
        {
            string html;
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("index.html"))
            using (var r = new StreamReader(s, Encoding.UTF8))
                html = r.ReadToEnd();

            string preload = "";
            if (args.Length > 0 && File.Exists(args[0]))
                preload = "window.PRELOAD_QUIZ=" + JsString(File.ReadAllText(args[0], Encoding.UTF8)) + ";";
            html = html.Replace("/*PRELOAD*/", preload);

            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JsonQuiz");
            Directory.CreateDirectory(dir);
            string file = Path.Combine(dir, "index.html");
            File.WriteAllText(file, html, new UTF8Encoding(false));

            string url = new Uri(file).AbsoluteUri;
            string browser = FindBrowser();
            if (browser != null)
                Process.Start(new ProcessStartInfo(browser, "--app=\"" + url + "\" --window-size=1050,860") { UseShellExecute = false });
            else
                Process.Start(file);
        }
        catch (Exception e)
        {
            MessageBox.Show("Couldn't start JSON Quiz:\n" + e.Message, "JSON Quiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    static string FindBrowser()
    {
        string pf86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        string pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string[] candidates = {
            Path.Combine(pf86, @"Microsoft\Edge\Application\msedge.exe"),
            Path.Combine(pf, @"Microsoft\Edge\Application\msedge.exe"),
            Path.Combine(pf, @"Google\Chrome\Application\chrome.exe"),
            Path.Combine(pf86, @"Google\Chrome\Application\chrome.exe"),
            Path.Combine(local, @"Google\Chrome\Application\chrome.exe"),
        };
        foreach (var c in candidates)
            if (File.Exists(c)) return c;
        return null;
    }

    // Encode text as a JS string literal that is safe inside a <script> tag.
    static string JsString(string s)
    {
        var sb = new StringBuilder("\"");
        foreach (char ch in s)
        {
            switch (ch)
            {
                case '\\': sb.Append("\\\\"); break;
                case '"': sb.Append("\\\""); break;
                case '\n': sb.Append("\\n"); break;
                case '\r': sb.Append("\\r"); break;
                case '\t': sb.Append("\\t"); break;
                case '<': sb.Append("\\u003c"); break;
                default:
                    // control chars plus U+2028/U+2029, which old JS engines treat as line breaks
                    if (ch < 0x20 || ch == 0x2028 || ch == 0x2029) sb.AppendFormat("\\u{0:x4}", (int)ch);
                    else sb.Append(ch);
                    break;
            }
        }
        return sb.Append('"').ToString();
    }
}
