using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HEICConverter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            HEICInvoke.read_profile();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            var filepicker = new OpenFileDialog();
            filepicker.Title = "Chọn file";
            filepicker.Multiselect = false;
            filepicker.Filter = "HEIF(.heic)|*.heic";
            if (filepicker.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var tempheicfile = HEICInvoke.read_heif(filepicker.FileName);
            int copysize = 0;
            pictureBoxView.Image = HEICInvoke.ImageFromByte(HEICInvoke.invoke_heif2jpg(tempheicfile, 50, "tmp/temp_bitstream.hevc", ref copysize, false, false));
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "jpeg";
            dialog.Filter = "*.jpg, *.jpeg|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxView.Image.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }
    }
}