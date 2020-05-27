using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.DAO;
using CMCS.Common.Entities; 
using CMCS.Common;
using CMCS.Common.Utilities;
using CMCS.CarTransport.Queue.Core;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Enums; 

namespace CMCS.CarTransport.Queue.Frms.Sys
{
    public partial class FrmLogin : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmLogin()
        {
            InitializeComponent();

            //StyleManager.MetroColorGeneratorParameters = MetroColorGeneratorParameters.BlackSky;
        }

        CommonDAO commonDao = CommonDAO.GetInstance();

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void FormInit()
        {
            //// �����û�
            //cmbUserAccount.DataSource = commonDao.GetAllSystemUser(eUserRoleCodes.�������ܻ�.ToString());
            //cmbUserAccount.DisplayMember = "UserName";
            //cmbUserAccount.ValueMember = "UserAccount";

            // �����û�
            cmbUserAccount.DataSource = commonDao.GetAllSystemUser();
            cmbUserAccount.DisplayMember = "UserName";
            cmbUserAccount.ValueMember = "UserAccount";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region ��֤

            if (cmbUserAccount.SelectedItem == null) return;
            if (string.IsNullOrEmpty(txtUserPassword.Text)) return;

            #endregion

            //User user = commonDao.Login(eUserRoleCodes.�������ܻ�.ToString(), cmbUserAccount.SelectedValue.ToString(), MD5Util.Encrypt(txtUserPassword.Text));
            CmcsUser user = commonDao.Login(cmbUserAccount.SelectedValue.ToString(), txtUserPassword.Text);
            if (user != null)
            {
                GlobalVars.LoginUser = user;

                this.Hide();

                SelfVars.MainFrameForm = new FrmMainFrame();
                SelfVars.MainFrameForm.Show();
            }
            else
            {
                MessageBoxEx.Show("�ʺŻ�����������������룡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtUserPassword.ResetText();
                txtUserPassword.Focus();
            }
        }
    }
}