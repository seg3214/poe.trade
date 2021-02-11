using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
namespace poe.trade_trader
{
    public partial class Form1 : Form
    {
        int sellstacksize, buystacksize, sellquant, buyquant;
        static int labellistaize = 60;
        Label[] buylabels = new Label[labellistaize];
        Label[] selllabels = new Label[labellistaize];
        PictureBox[] buypics = new PictureBox[labellistaize];
        PictureBox[] sellpics = new PictureBox[labellistaize];
        List<Offers> offers = new List<Offers>();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSaveOffers(Boolean Save)
        {
            string f = @"assets\data.xml";
            if (Save)
            {
                XmlWriter xmlWriter = XmlWriter.Create(f);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Offers");
                foreach (Offers s in offers)
                {
                    xmlWriter.WriteStartElement("Offer");
                    xmlWriter.WriteAttributeString("sellratio", string.Format("{0:0.0000}", s.sellratio));
                    xmlWriter.WriteAttributeString("BuyCurrency", s.BuyCurrency);
                    xmlWriter.WriteAttributeString("BuyCurrencyAmount", s.BuyCurrencyAmount.ToString());
                    xmlWriter.WriteAttributeString("SellCurrencyAmount", s.SellCurrencyAmount.ToString());
                    xmlWriter.WriteAttributeString("SellCurrency", s.SellCurrency);
                    xmlWriter.WriteAttributeString("buyratio", string.Format("{0:0.0000}", s.buyratio));
                    xmlWriter.WriteAttributeString("BuyStackSize", s.BuyStackSize.ToString());
                    xmlWriter.WriteAttributeString("SellStackSize", s.SellStackSize.ToString());
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            else {
                offers.Clear();
                XmlReader xmlReader = XmlReader.Create(f);
                while (xmlReader.Read())
                {
                    if (xmlReader.Name.Equals("Offer") && (xmlReader.NodeType == XmlNodeType.Element))
                    {
                        if (xmlReader.HasAttributes)
                            offers.Add(
                                new Offers(
                                    xmlReader.GetAttribute("BuyCurrency"), xmlReader.GetAttribute("SellCurrency"), Int32.Parse(xmlReader.GetAttribute("BuyCurrencyAmount")), Int32.Parse(xmlReader.GetAttribute("SellCurrencyAmount")), Int32.Parse(xmlReader.GetAttribute("BuyStackSize")), Int32.Parse(xmlReader.GetAttribute("SellStackSize"))
                                    )
                                );
                    }

                }
                listBox1.Items.Clear();
                xmlReader.Close();
                foreach (Offers s in offers)
                {
                    listBox1.Items.Add("(" + string.Format("{0:0.0000}", s.sellratio) + ") " + s.BuyCurrency + " " + s.BuyCurrencyAmount + " = " + s.SellCurrencyAmount + " " + s.SellCurrency + " (" + string.Format("{0:0.0000}", s.buyratio) + ")");
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (Currencies.curr c in Currencies.CurrencyList)
            {
                comboBox1.Items.Add(c.Name);
                comboBox2.Items.Add(c.Name);
            }
            SellPictureBox.Image = Image.FromFile(@"assets\images\tr.png");
            BuyPictureBox.Image = Image.FromFile(@"assets\images\tr.png");
            LoadSaveOffers(false);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            /* for (int i = comboBox1.Items.Count - 1; i > 0; i--)
             {
                 if (comboBox1.Items[i].Value.Contains(textBox5.Text))
                 {
                     comboBox1.Items[i].Selected = true;
                     break;
                 }
             }*/
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            // comboBox1.SelectedIndex = comboBox1.Contains(textBox5.Text);
            for (int i = comboBox1.Items.Count - 1; i > 0; i--)
            {
                if (comboBox1.Items[i].ToString().ToLower().Contains(textBox5.Text))
                {
                    comboBox1.SelectedItem = comboBox1.Items[i];
                    break;
                }
            }
            //comboBox1.Items.ToString().Contains("three")
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static void DoEventsWait(int mil)
        {
            DateTime ls = DateTime.Now;
            while (DateTime.Now.Subtract(ls).TotalMilliseconds < mil)
                Application.DoEvents();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //textBox3.Text = "144";
            // textBox5.Text = "chrom";
            // DoEventsWait(500);
            //  FillBuyArea();
            // Create the XmlDocument.
            //  listBox1.Items.Add("(10.3333) Orb of Alchemy 31 = 3 Orb of Alchemy(0.0968)");

            XmlWriter xmlWriter = XmlWriter.Create(@"assets\data.xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Offers");
            foreach (Offers s in offers)
            {
                xmlWriter.WriteStartElement("Offer");
                xmlWriter.WriteAttributeString("sellratio", string.Format("{0:0.0000}", s.sellratio));
                xmlWriter.WriteAttributeString("BuyCurrency", s.BuyCurrency);
                xmlWriter.WriteAttributeString("BuyCurrencyAmount", s.BuyCurrencyAmount.ToString());
                xmlWriter.WriteAttributeString("SellCurrencyAmount", s.SellCurrencyAmount.ToString());
                xmlWriter.WriteAttributeString("SellCurrency", s.SellCurrency);
                xmlWriter.WriteAttributeString("buyratio", string.Format("{0:0.0000}", s.buyratio));
                //xmlWriter.WriteString("John Doe");
                xmlWriter.WriteEndElement();

                //  xmlWriter.WriteStartElement("user");
                //   xmlWriter.WriteAttributeString("age", "39");
                //  xmlWriter.WriteString("Jane Doe");
            }
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            /*   XmlDocument doc = new XmlDocument();
               XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Offers"));
               foreach (Offers s in offers)
               { 
                   el.
                   el.AppendChild(doc.CreateElement("Nested"))..InnerText = "data";
                   //XmlElement el = (XmlElement)el2.AppendChild(doc.CreateElement("Offer"));
                   el.SetAttribute("sellratio", string.Format("{0:0.0000}", s.sellratio));
                   el.SetAttribute("BuyCurrency", s.BuyCurrency);
                   el.SetAttribute("BuyCurrencyAmount", s.BuyCurrencyAmount.ToString());
                   el.SetAttribute("SellCurrencyAmount", s.SellCurrencyAmount.ToString());
                   el.SetAttribute("SellCurrency", s.SellCurrency);
                   el.SetAttribute("buyratio", string.Format("{0:0.0000}", s.buyratio));
               }

               //el.SetAttribute("Bar", "some & value");
               Console.WriteLine(doc.OuterXml);
               doc.Save("data.xml");*/
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            for (int i = comboBox2.Items.Count - 1; i > 0; i--)
            {
                if (comboBox2.Items[i].ToString().ToLower().Contains(textBox6.Text))
                {
                    comboBox2.SelectedItem = comboBox2.Items[i];
                    break;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sellstacksize = Int32.Parse(getBetween(comboBox2.Text, "[", "]"));
            sellstacksize = Currencies.CurrencyList.Find(x => x.Name == comboBox2.Text).stacksize;
            if (sellquant > 0 && sellstacksize > 0) FillArea(true, ref selllabels, ref sellpics, sellquant, sellstacksize, ref SellPictureBox);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            FillLabels(true);
            if (sellquant > 0 && sellstacksize > 0) FillArea(true, ref selllabels, ref sellpics, sellquant, sellstacksize, ref SellPictureBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                offers.Add(new Offers(comboBox2.Text, comboBox1.Text, Int32.Parse(textBox4.Text), Int32.Parse(textBox3.Text), Currencies.CurrencyList.Find(x => x.Name == comboBox2.Text).stacksize, Currencies.CurrencyList.Find(x => x.Name == comboBox1.Text).stacksize));
                listBox1.Items.Clear();
                foreach (Offers s in offers)
                {
                    listBox1.Items.Add("(" + string.Format("{0:0.0000}", s.sellratio) + ") " + s.BuyCurrency + " " + s.BuyCurrencyAmount + " = " + s.SellCurrencyAmount + " " + s.SellCurrency + " (" + string.Format("{0:0.0000}", s.buyratio) + ")");
                }
            }
            catch { }
            //listBox1.Items.Add("buysstackzie "+ comboBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                offers.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.Remove(listBox1.SelectedItem);
                listBox1.Refresh();
            }
            catch { }
        }

        private void FillLabels(bool s)
        {
            float g = 0, g1 = 0;
            try
            { 
                if (buyquant > 0 && sellquant > 0)
                {
                    buyquant = Int32.Parse(textBox3.Text);
                    sellquant = Int32.Parse(textBox4.Text);
                    g = (float)sellquant / buyquant;
                    label3.Text = string.Format("{0:0.0000}", g);

                    g1 = (float)buyquant / sellquant;
                    label4.Text = string.Format("{0:0.0000}", g1);

                } else
                 if (!s)
                { buyquant = Int32.Parse(textBox3.Text);
                    g = (float)sellquant / buyquant;
                    label3.Text = string.Format("{0:0.0000}", g);
                }
                else
                { sellquant = Int32.Parse(textBox4.Text);
                    g = (float)buyquant / sellquant;
                    label4.Text = string.Format("{0:0.0000}", g);
                }
            }
            catch { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadSaveOffers(true);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            offers.Clear();
            XmlReader xmlReader = XmlReader.Create(@"assets\data.xml");
            while (xmlReader.Read())
            {
                //keep reading until we see your element
                if (xmlReader.Name.Equals("Offer") && (xmlReader.NodeType == XmlNodeType.Element))
                {
                    //if (xmlReader.HasAttributes)
                    //  offers.Add(new Offers(xmlReader.GetAttribute("BuyCurrency"), xmlReader.GetAttribute("SellCurrency"),Int32.Parse( xmlReader.GetAttribute("BuyCurrencyAmount")), Int32.Parse(xmlReader.GetAttribute("SellCurrencyAmount"))));
                    //string name = xmlReader.GetAttribute("sellratio");
                    // --> now **add to collection** - or whatever
                    //  listBox1.Items.Add(name);
                }
            }
            listBox1.Items.Clear();
            xmlReader.Close();
            foreach (Offers s in offers)
            {
                listBox1.Items.Add("(" + string.Format("{0:0.0000}", s.sellratio) + ") " + s.BuyCurrency + " " + s.BuyCurrencyAmount + " = " + s.SellCurrencyAmount + " " + s.SellCurrency + " (" + string.Format("{0:0.0000}", s.buyratio) + ")");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            ClearPictureBox(false);
            ClearPictureBox(true);
            comboBox2.SelectedIndex = comboBox2.FindString(offers[listBox1.SelectedIndex].BuyCurrency);
            comboBox1.SelectedIndex = comboBox1.FindString(offers[listBox1.SelectedIndex].SellCurrency);
            textBox4.Text = offers[listBox1.SelectedIndex].BuyCurrencyAmount.ToString();
            textBox3.Text = offers[listBox1.SelectedIndex].SellCurrencyAmount.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearPictureBox(true); ClearPictureBox(false);
                if (listBox1.SelectedIndex > -1 && textBox1.Text != "")
                {
                    int s1 = Int32.Parse(textBox1.Text);
                    float d1 = s1 / offers[listBox1.SelectedIndex].sellratio;

                    double fg = (s1 / offers[listBox1.SelectedIndex].BuyStackSize);
                    int stacks = (int)Math.Truncate(fg);
                    int spares = s1 - (stacks * offers[listBox1.SelectedIndex].BuyStackSize);
                    label6.Text = textBox1.Text + " " + offers[listBox1.SelectedIndex].BuyCurrency + " ( " + stacks + " stacks and " + spares + " )";

                    fg = (d1 / offers[listBox1.SelectedIndex].SellStackSize);
                    stacks = (int)Math.Truncate(fg);
                    spares = (int)Math.Round(d1 - (stacks * offers[listBox1.SelectedIndex].SellStackSize));
                    label9.Text = string.Format("{0:0.00}", d1) + " " + offers[listBox1.SelectedIndex].SellCurrency + " ( " + stacks + " stacks and " + spares + " )"; ;
                    //label7.Text = string.Format("{0:0c.00}", d1) + " of " + offers[listBox1.SelectedIndex].SellCurrency;
                    //label8.Text = stacks + " and " + spares;

                    //   buyquant = Int32.Parse(textBox3.Text);
                    //   sellquant = Int32.Parse(textBox4.Text);
                    // d.CurrencyList.Find(x => x.Name == comboBox2.Text).stacksize;
                    if (s1 > 0)
                    //    comboBox2.SelectedIndex = d.CurrencyList.Find(x => x.Name == offers[listBox1.SelectedIndex].BuyCurrency).Name ;
                  //      textBox4.Text = s1;

                        FillArea(true, ref selllabels, ref sellpics, s1, offers[listBox1.SelectedIndex].BuyStackSize, ref SellPictureBox);
                    if (d1 > 0)
                        FillArea(false, ref selllabels, ref sellpics, (int)Math.Round(d1), offers[listBox1.SelectedIndex].SellStackSize, ref BuyPictureBox);
                }
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex > -1 && textBox2.Text != "")
                {
                    int s1 = Int32.Parse(textBox2.Text);
                    float d1 = s1 / offers[listBox1.SelectedIndex].buyratio;

                    double fg = (s1 / offers[listBox1.SelectedIndex].SellStackSize);
                    int stacks = (int)Math.Truncate(fg);
                    int spares = s1 - (stacks * offers[listBox1.SelectedIndex].SellStackSize);
                    label6.Text = textBox2.Text + " " + offers[listBox1.SelectedIndex].SellCurrency + " ( " + stacks + " stacks and " + spares + " )";

                    fg = (d1 / offers[listBox1.SelectedIndex].BuyStackSize);
                    stacks = (int)Math.Truncate(fg);
                    spares = (int)Math.Round(d1 - (stacks * offers[listBox1.SelectedIndex].BuyStackSize));
                    label9.Text = string.Format("{0:0.00}", d1) + " " + offers[listBox1.SelectedIndex].BuyCurrency + " ( " + stacks + " stacks and " + spares + " )"; ;

                    if (s1 > 0)
                        FillArea(false, ref selllabels, ref sellpics, s1, offers[listBox1.SelectedIndex].SellStackSize, ref BuyPictureBox);
                    if (d1 > 0)
                        FillArea(true, ref selllabels, ref sellpics, (int)Math.Round(d1), offers[listBox1.SelectedIndex].BuyStackSize, ref SellPictureBox);
                }
            }
            catch { }
        }
        private void ClearPictureBox(Boolean sell)
        {
            //PictureBox pbox=null; Label[] ll=null; PictureBox[] pl=null;
            if (sell)
            {
                for (int i = 0; i < labellistaize; i++)
                {
                    BuyPictureBox.Controls.Remove(buylabels[i]);
                    BuyPictureBox.Controls.Remove(buypics[i]);
                }
            }
            else
            {
                for (int i = 0; i < labellistaize; i++)
                {
                    SellPictureBox.Controls.Remove(selllabels[i]);
                    SellPictureBox.Controls.Remove(sellpics[i]);

                }
            }   
             }

        private void button5_Click(object sender, EventArgs e)
        {
         //   WebRequest webRequest = WebRequest.Create("http://ussbazesspre004:9002/DREADD?" + fileName);
          //  WebResponse webResp = webRequest.GetResponse();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FillLabels(false);
           
            if (buyquant > 0 && buystacksize > 0) FillArea(false, ref buylabels, ref buypics, buyquant, buystacksize, ref BuyPictureBox);
        }
        private void FillArea(bool s, ref Label[] llist, ref PictureBox[] plist, int Quant, int Stacksize, ref PictureBox pbox)
        {
            //imageList1.Images.Add("ccc", Image.FromFile(@"images\Chaos_Orb.png"));
            ClearPictureBox(!s);
            int stacks = 0;
            int spares = 0;
            int g = 0;
            try
            {
                //int buyquant = Int32.Parse(textBox3.Text);
                // int stacksize = Int32.Parse(textBox2.Text);
                double fg = (Quant / Stacksize);
                stacks = (int)Math.Truncate(fg);
                spares = Quant - (stacks * Stacksize);
                if (spares != 0) g = 1;
                if (!s) if (comboBox1.SelectedIndex == -1) return;
                    else
                     label1.Text = stacks.ToString() + " stacks and " + spares.ToString();
                else
                    if (comboBox2.SelectedIndex == -1)
                    return;

                else
                    label2.Text = stacks.ToString() + " stacks and " + spares.ToString();

            }
            catch { }
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                try
                {
                    label3.ForeColor = Color.Black; ;
                    label4.ForeColor = Color.Black;
                    Offers of = offers.Find(x => x.BuyCurrency == comboBox2.Text && x.SellCurrency == comboBox1.Text);
                    if (of != null)
                    {
                        float rat = (float)of.BuyCurrencyAmount / of.SellCurrencyAmount;
                        float rat1 = (float)sellquant/buyquant   ;
                        if (rat == rat1)
                        {
                            label3.ForeColor = Color.Green;
                            label4.ForeColor = Color.Green;
                        }
                        else {
                            label3.ForeColor = Color.Red;
                            label4.ForeColor = Color.Red;
                        }
                        //  label3.Text = string.Format("{0:0.0000}", g);
                       // listBox1.Items.Add(string.Format("{0:0.0000}", rat)+ "="+string.Format("{0:0.0000}", rat1));
                    }
                }
                catch { };
                }
            if (stacks > labellistaize - 1)  return; ;

     /*       for (int i = 0; i < labellistaize; i++)
            {
                pbox.Controls.Remove(llist[i]);
                pbox.Controls.Remove(plist[i]);

            }*/

            int pshift = 53;
            int rowcount = 1, colcount = 1;
            Point pxy = new Point(0, 0);
            int _absoluteImagePositionX = pbox.Width / 2 - pbox.Image.Width / 2;
            int _absoluteImagePositionY = pbox.Height / 2 - pbox.Image.Height / 2;

            for (int i = 0; i < stacks + g; i++)
            {
                if (rowcount == 6) { colcount++; rowcount = 1;
                    pxy.X = pxy.X + pshift; pxy.Y = (_absoluteImagePositionY + 13) - pshift;
                }
                llist[i] = new Label();
                plist[i] = new PictureBox();
                if (i == 0)
                {
                    // plist[i].Location = new Point(pbox.Location.X, pbox.Location.Y - 8);
                    plist[i].Location = new Point(_absoluteImagePositionX + 13, _absoluteImagePositionY + 13);
                    llist[i].Location = new Point(_absoluteImagePositionX + 13, _absoluteImagePositionY + 13);
                    pxy = plist[i].Location;
                    // listBox1.Items.Add(pbox...Left + " " +(pbox.Top - 8));
                }
                else
                {
                    plist[i].Location = new Point(pxy.X, pxy.Y + (pshift));
                    llist[i].Location = new Point(pxy.X, pxy.Y + (pshift));
                    pxy.Y = pxy.Y + pshift;
                }
                if (!s)
                { plist[i].Name = "pic" + i;
                    llist[i].Name = "lab" + i;
                    plist[i].Image = Currencies.CurrencyList.Find(x => x.Name == comboBox1.Text).Image;
                }
                else
                { plist[i].Name = "picz" + i;
                    llist[i].Name = "labz" + i;
                    plist[i].Image = Currencies.CurrencyList.Find(x => x.Name == comboBox2.Text).Image;
                }
                plist[i].Size = new Size(52, 52);
                plist[i].SizeMode = PictureBoxSizeMode.StretchImage;
                
                // this.Controls.Add(pics[i]);
                plist[i].Parent = pbox;
                llist[i].Parent = pbox;
                llist[i].BringToFront();
                plist[i].BackColor = Color.Transparent;
                //    return;
                // llist[i].Name = "LabelStack" + i.ToString();
                if (i == stacks)
                {
                    llist[i].Text = spares.ToString();
                }
                else
                {
                    llist[i].Text = Stacksize.ToString();
                    llist[i].ForeColor = Color.Red;
                }
                // llist[i].Location = new Point(colcount*52, rowcount * 52);
                llist[i].Size = new Size(25, 16);
                rowcount++;
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Text=comboBox1.Items[comboBox1.SelectedIndex].ToString();
            buystacksize = Currencies.CurrencyList.Find(x => x.Name == comboBox1.Text).stacksize;
            if (buyquant > 0 && buystacksize > 0) FillArea(false, ref buylabels, ref buypics, buyquant, buystacksize, ref BuyPictureBox);
            // FillBuyArea();
            
        }
    }
}
