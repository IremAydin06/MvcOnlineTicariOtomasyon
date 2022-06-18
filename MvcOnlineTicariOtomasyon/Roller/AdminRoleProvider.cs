using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Roller
{
    public class AdminRoleProvider : RoleProvider  //RoleProvider isminde bir interface'imiz var buradan miras alması gerekiyor.
    {
        //Adminin A ve B gibi rolleri olacak.
        //Rollere göre yetkisinin olduğu işlemler değişecek.
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        //************************************* İŞLEMLERİMİZİ BURA ÜZERİNDE YAPACAĞIZ *****************************
        public override string[] GetRolesForUser(string username)
        {
            Context c = new Context();
            var k = c.Admins.FirstOrDefault(x => x.KullaniciAd == username); //username: sisteme giriş yaparken kullandığımız kullanıcı adı.
            return new string[] { k.Yetki }; //Giriş yapan kullanıcının yetkilerini döndür. Döndürülen yetkiyi DepartmanController'daki [Authorize(Roles = "A")] kısmı karşılayacak.
        }

        //**********************************************************************************************************

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}