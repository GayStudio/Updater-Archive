using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MCUpdater
{
    public partial class regionCalculator : Form
    {
        public regionCalculator()
        {
            InitializeComponent();
        }

        public void error(string msg, string title = "错误")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void getMcrInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(X.Text) || string.IsNullOrEmpty(Z.Text))
            {
                error("若要用坐标计算区域文件信息\r\n请填写 X 和 Z 坐标");
                return;
            }
            try
            {
                int x;
                int z;
                if (typeB.Checked)
                {
                    x = (int)Math.Floor(decimal.Parse(X.Text) / 16 / 32);
                    z = (int)Math.Floor(decimal.Parse(Z.Text) / 16 / 32);
                }
                else
                {
                    x = (int)Math.Floor(decimal.Parse(X.Text) / 32);
                    z = (int)Math.Floor(decimal.Parse(Z.Text) / 32);
                }
                rX.Text = x.ToString();
                rZ.Text = z.ToString();
                rF.Text = "r." + rX.Text + "." + rZ.Text + ".mca";
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }

        private void getPosInfo_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(rX.Text) || string.IsNullOrEmpty(rZ.Text)) && string.IsNullOrEmpty(rF.Text))
            {
                error("用区域文件信息计算坐标信息\r\n请填写 X 和 Z 信息");
                return;
            }
            try
            {
                int x = 0;
                int z = 0;
                if (typeB.Checked)
                {
                    x = int.Parse(rX.Text) * 16 * 32;
                    z = int.Parse(rZ.Text) * 16 * 32;
                }
                else
                {
                    x = int.Parse(rX.Text) * 32;
                    z = int.Parse(rZ.Text) * 32;
                }
                rF.Text = "r." + rX.Text + "." + rZ.Text + ".mca";
                X.Text = x.ToString();
                Z.Text = z.ToString();
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }
    }
}
