﻿using DAN_XLVI_Milica_Karetic.Command;
using DAN_XLVI_Milica_Karetic.Model;
using DAN_XLVI_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLVI_Milica_Karetic.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;
        Service service = new Service();

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
            user = new tblUser();
            UserList = service.GetAllUsers().ToList();
        }
        #endregion

        #region Property
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }
        #endregion

        #region Commands
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;
            bool found = false;
            if (UserList.Any())
            {
                for (int i = 0; i < UserList.Count; i++)
                {
                    if (User.Username == UserList[i].Username && password == UserList[i].UserPassword)
                    {
                        Service.LoggedInUser.Add(UserList[i]);
                        MainWindow mw = new MainWindow();
                        InfoLabel = "Loggedin";
                        found = true;
                        view.Close();
                        mw.Show();
                        break;
                    }
                }

                if (found == false)
                {
                    InfoLabel = "Wrong Username or Password";
                }
            }
            else
            {
                InfoLabel = "Database is empty";
            }

            if (User.Username == "WPFadmin" && password == "WPFadmin")
            {
                Admin admin = new Admin();
                view.Close();
                admin.Show();
            }
        }
        #endregion
    }
}
