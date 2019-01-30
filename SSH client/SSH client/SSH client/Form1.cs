using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci;


namespace SSH_client
{
    public partial class Form1 : Form
    {
        public int i=0;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "192.168.88.72";
            textBox2.Text = "any";
            textBox3.Text = "Mavr211354";
            
        }
        internal class RunScript {

            [JsonProperty("action")]
            public string ScriptAction  { get; set; }

            [JsonProperty("script")]
            public string ScriptString  { get; set; }

        }

      

        internal class Server
        {

            [JsonProperty("address")]
            public string IPAdd { get; set; }
           
            [JsonProperty("port")]
            public string Port { get; set; }

            [JsonProperty("user")]
            public string UserName { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

        }



        private void Form1_Load(object sender, EventArgs e)
        {


        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            SshClient ssh_Client = new SshClient(textBox1.Text, 22, textBox2.Text, textBox3.Text);
            try
            {
                ssh_Client.Connect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(),"Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

            foreach (var item in listBox1.Items)
            {
                SshCommand x = ssh_Client.RunCommand(item.ToString());
                textBox4.Text += "\r\n" + x.Result;
            }


           

            ssh_Client.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            int step = 0;
            bool isNumeric = int.TryParse(textBox6.Text, out step );

            if (isNumeric)
            {
                for (int n = i; n < i + step; n++)
                {
                    int qm = n + 200;
                    listBox1.Items.Insert(n, textBox5.Text );
                }
                i += step;
            }
            else
            {
                MessageBox.Show("counter value isn't numeric","Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox6.Text = "";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text += "\r\n"+listBox1.SelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)//clear button
        {
            listBox1.Items.Clear();
            i = 0;
        }
    }
}
