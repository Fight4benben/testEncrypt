using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acrel.Security;
using System.IO;

namespace testEncrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            this.registerDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string key = this.serialNo.Text.Trim();
            string proName = this.projectName.Text.Trim();
            string regDate = this.registerDate.Text.Trim();

            if(!checkKey(key))
                return;

            string value = RClass.Encryptor(key+";"+proName+";"+regDate);

            string path = AppDomain.CurrentDomain.BaseDirectory +@"\" + DateTime.Now.ToString("yyyy-MM-dd")+"_"+proName+@"\Register.re";

            WriteTxt(path, @"\Register.re",value);

            MessageBox.Show("生成成功！");
        }

        private bool checkKey(string key)
        {
            if (key.Length != 29)
            {
                MessageBox.Show("注册码位数不对，请重新输入！");
                return false;
            }

            string[] segs = key.Split('-');

            if (segs.Length != 5)
            {
                MessageBox.Show("注册码段数不对！");
                return false;
            }
                

            foreach (string seg in segs)
            {
                if (seg.Length != 5)
                {
                    MessageBox.Show("每一段注册码的位数不为5");
                    return false;
                }
            }

            return true;
        }

        private void WriteTxt(string filename,string name,string str)
        {
            string path = filename.Replace(name, "");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(filename))
            {
                FileStream fscreate = File.Create(filename);

                //创建文件后立即释放资源
                fscreate.Close();
            }

            FileStream fs = File.Open(filename, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
