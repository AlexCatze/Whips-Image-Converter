using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhipsImageConverter
{
    public partial class ResultForm : Form
    {
        string fulltext;
        List<string> parts = new List<string>();
        public ResultForm(string f)
        {
            InitializeComponent();
            fulltext = f;
        }
        /*
        private string Compress(string str)
        {
            string res = "";
            var arr = str.ToCharArray();
            char prev = ' ';
            int count = 0;
            foreach(char c in arr)
            {
                /*
                if(c != prev)
                {
                    if (count > 7)
                    {
                        res += 'ї' + count.ToString() + 'ї' + c;
                    }
                    else
                        res += prev * count;
                    prev = c;
                    count = 1;
                }
                else
                {
                    count++;
                }*//*
                res += c;
            }
            return res;
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            //fulltext = Compress(fulltext);
            while(fulltext.Length > 64000)
            {
                parts.Add(fulltext.Substring(0, 64000));
                fulltext = fulltext.Substring(64000);
            }
            parts.Add(fulltext);
            numericUpDown1.Maximum = parts.Count;
            label1.Text = "Blocks: " + parts.Count;
            textBox1.Text = parts[(int)(numericUpDown1.Value - 1)];
            textBox2.Text = DateTime.Now.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = parts[(int)(numericUpDown1.Value - 1)];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    Clipboard.SetText(textBox1.Text, TextDataFormat.UnicodeText);
                }
                catch
                { }
                //Clipboard.SetDataObject(textBox_Return.Text, true, 2, 100);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sbc = "";
            sbc += "<?xml version=\"1.0\"?><Definitions xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><ShipBlueprints><ShipBlueprint xsi:type=\"MyObjectBuilder_ShipBlueprintDefinition\"><Id Type=\"MyObjectBuilder_ShipBlueprintDefinition\" Subtype=\"";
            sbc += textBox2.Text;
            sbc += "\" /><DisplayName>MovieConverter</DisplayName><CubeGrids><CubeGrid><SubtypeName /><EntityId>110089151797373483</EntityId><PersistentFlags>CastShadows InScene</PersistentFlags><PositionAndOrientation><Position x=\"-788072.70302614581\" y=\"219508.78941113973\" z=\"1955107.4238509971\" /><Forward x=\"-0.996565938\" y=\"-0.08259557\" z=\"0.0058313976\" /><Up x=\"0.0821430162\" y=\"-0.9950517\" z=\"-0.05589332\" /><Orientation><X>0.709102</X><Y>0.009491104</Y><Z>0.7033494</Z><W>-0.04882546</W></Orientation></PositionAndOrientation><LocalPositionAndOrientation xsi:nil=\"true\" /><GridSizeEnum>Large</GridSizeEnum><CubeBlocks>";
            //
            int y = 0;
            string name = "", data = "";
            sbc += ArmBlock(y);
            sbc += PanBlock(1, y, -1, name, data);
            sbc += PanBlock(1, y, 1, name, data);
            sbc += PanBlock(-1, y, 1, name, data);
            sbc += PanBlock(-1, y, -1, name, data);

            //
        }

        string PanBlock(int x,int y,int z, string name,string data)
        {
            var res = "";
            res+= "<MyObjectBuilder_CubeBlock xsi:type=\"MyObjectBuilder_TerminalBlock\"><SubtypeName>ControlPanel</SubtypeName><EntityId>138683951383717336</EntityId><Min x=\"";
            res += x;
            res += "\" y = \"";
            res += y;
            res += "\" z = \"";
            res += z;
            res += "\" /><BlockOrientation Forward=\"Left\" Up=\"Down\" /><Owner>144115188075855895</Owner><BuiltBy>144115188075855895</BuiltBy><ShareMode>Faction</ShareMode><ComponentContainer><Components><ComponentData><TypeId>MyModStorageComponentBase</TypeId><Component xsi:type=\"MyObjectBuilder_ModStorageComponent\"><Storage><dictionary><item><Key>74de02b3-27f9-4960-b1c4-27351f2b06d1</Key><Value>";
            res += data;
            res += "</Value></item></dictionary></Storage></Component></ComponentData></Components></ComponentContainer><CustomName>";
            res += name;
            res += "</CustomName><ShowOnHUD>false</ShowOnHUD><ShowInTerminal>false</ShowInTerminal><ShowInToolbarConfig>false</ShowInToolbarConfig><ShowInInventory>false</ShowInInventory></MyObjectBuilder_CubeBlock>";
            return res;
        }

        string ArmBlock(int y)
        {
            var res = "";
            res += "<MyObjectBuilder_CubeBlock xsi:type=\"MyObjectBuilder_CubeBlock\"><SubtypeName>LargeBlockArmorBlock</SubtypeName><Min x=\"0\" y=\"";
            res += 0;
            res += "\" z=\"0\" /><BuiltBy>144115188075855895</BuiltBy></MyObjectBuilder_CubeBlock>";
            return res;
        }
    }
}
