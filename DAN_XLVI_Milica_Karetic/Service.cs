using DAN_XLVI_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAN_XLVI_Milica_Karetic
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        /// <summary>
        /// Saves the user that is logged in
        /// </summary>
        public List<tblUser> LoggedInUser = new List<tblUser>();

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>Users list</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all workers from database
        /// </summary>
        /// <returns>workers list</returns>
        public List<tblUser> GetAllWorkers()
        {
            try
            {
                List<tblUser> list = new List<tblUser>();
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access == null)
                        {
                            list.Add(GetAllUsers()[i]);
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all managers from database
        /// </summary>
        /// <returns>managers list</returns>
        public List<tblUser> GetAllManagers()
        {
            try
            {
                List<tblUser> list = new List<tblUser>();
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    for (int i = 0; i < GetAllUsers().Count; i++)
                    {
                        if (GetAllUsers()[i].Access != null)
                        {
                            list.Add(GetAllUsers()[i]);
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all information about reports
        /// </summary>
        /// <returns>reports list</returns>
        public List<tblReport> GetAllReports()
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    List<tblReport> list = new List<tblReport>();
                    list = (from x in context.tblReports select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Creates or edits a worker
        /// </summary>
        /// <param name="worker">worker to add or edit</param>
        /// <returns>a new or edited worker</returns>
        public vwUser AddWorker(vwUser worker)
        {
            InputCalculator iv = new InputCalculator();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    if (worker.UserID == 0)
                    {
                        worker.DateOfBirth = iv.CountDateOfBirth(worker.JMBG);

                        tblUser newWorker = new tblUser();
                        newWorker.FirstName = worker.FirstName;
                        newWorker.LastName = worker.LastName;
                        newWorker.JMBG = worker.JMBG;
                        newWorker.DateOfBirth = worker.DateOfBirth;
                        newWorker.BankAccount = worker.BankAccount;
                        newWorker.Email = worker.Email;
                        newWorker.Position = worker.Position;
                        newWorker.Salary = worker.Salary;
                        newWorker.Username = worker.Username;
                        newWorker.UserPassword = worker.UserPassword;

                        context.tblUsers.Add(newWorker);
                        context.SaveChanges();
                        worker.UserID = newWorker.UserID;

                        return worker;
                    }
                    else
                    {
                        tblUser workerToEdit = (from ss in context.tblUsers where ss.UserID == worker.UserID select ss).First();

                        // Get the date of birth
                        worker.DateOfBirth = iv.CountDateOfBirth(worker.JMBG);

                        workerToEdit.FirstName = worker.FirstName;
                        workerToEdit.LastName = worker.LastName;
                        workerToEdit.JMBG = worker.JMBG;
                        workerToEdit.DateOfBirth = worker.DateOfBirth;
                        workerToEdit.BankAccount = worker.BankAccount;
                        workerToEdit.Email = worker.Email;
                        workerToEdit.Salary = worker.Salary;
                        workerToEdit.Username = worker.Username;
                        workerToEdit.Position = worker.Position;
                        workerToEdit.UserPassword = worker.UserPassword;

                        workerToEdit.UserID = worker.UserID;

                        tblUser workerEdit = (from ss in context.tblUsers
                                              where ss.UserID == worker.UserID
                                              select ss).First();
                        context.SaveChanges();
                        return worker;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">user id</param>
        /// <returns>True if the user exists</returns>
        public bool IsUserID(int userID)
        {
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    int result = (from x in context.tblUsers where x.UserID == userID select x.UserID).FirstOrDefault();

                    if (result != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Creates or edits a manager
        /// </summary>
        /// <param name="manager">manager to add or edit</param>
        /// <returns>a new or edited manager</returns>
        public vwManager AddManager(vwManager manager)
        {
            InputCalculator iv = new InputCalculator();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    if (manager.UserID == 0)
                    {
                        manager.DateOfBirth = iv.CountDateOfBirth(manager.JMBG);

                        tblUser newManager = new tblUser();
                        newManager.FirstName = manager.FirstName;
                        newManager.LastName = manager.LastName;
                        newManager.JMBG = manager.JMBG;
                        newManager.DateOfBirth = manager.DateOfBirth;
                        newManager.BankAccount = manager.BankAccount;
                        newManager.Email = manager.Email;
                        newManager.Position = manager.Position;
                        newManager.Salary = manager.Salary;
                        newManager.Username = manager.Username;
                        newManager.UserPassword = manager.UserPassword;
                        newManager.Sector = manager.Sector;
                        newManager.Access = manager.Access;

                        context.tblUsers.Add(newManager);
                        context.SaveChanges();
                        manager.UserID = newManager.UserID;
                        return manager;
                    }
                    else
                    {
                        tblUser managerToEdit = (from ss in context.tblUsers where ss.UserID == manager.UserID select ss).First();

                        // Get the date of birth
                        manager.DateOfBirth = iv.CountDateOfBirth(manager.JMBG);

                        managerToEdit.FirstName = manager.FirstName;
                        managerToEdit.LastName = manager.LastName;
                        managerToEdit.JMBG = manager.JMBG;
                        managerToEdit.DateOfBirth = manager.DateOfBirth;
                        managerToEdit.BankAccount = manager.BankAccount;
                        managerToEdit.Email = manager.Email;
                        managerToEdit.Salary = manager.Salary;
                        managerToEdit.Username = manager.Username;
                        managerToEdit.Position = manager.Position;
                        managerToEdit.UserPassword = manager.UserPassword;
                        managerToEdit.Sector = manager.Sector;
                        managerToEdit.Access = manager.Access;

                        managerToEdit.UserID = manager.UserID;

                        tblUser managerEdit = (from ss in context.tblUsers
                                               where ss.UserID == manager.UserID
                                               select ss).First();
                        context.SaveChanges();
                        return manager;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        

        /// <summary>
        /// Deletes user, users records and identification card depending if the uderID already exists
        /// </summary>
        /// <param name="user">the user that is being deleted</param>
        /// <returns>list of users</returns>
        public void DeleteWorker(int userID)
        {
            InputCalculator iv = new InputCalculator();
            List<tblReport> AllRportss = GetAllReports();
            try
            {
                using (ReportDBEntities context = new ReportDBEntities())
                {
                    bool isUser = IsUserID(userID);
                    
                    for (int i = 0; i < AllRportss.Count; i++)
                    {
                        if (AllRportss[i].UserID == userID)
                        {
                            tblReport report = (from r in context.tblReports where r.UserID == userID select r).First();
                            context.tblReports.Remove(report);
                            context.SaveChanges();
                        }
                    }
                    if (isUser == true)
                    {
                        // find the user and identification card before removing them
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        context.tblUsers.Remove(userToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the user");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
