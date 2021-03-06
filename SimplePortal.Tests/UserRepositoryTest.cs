﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimplePortal.Db.Ef;
using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void CreateUser_WithoutIdAndGuid_Should_Succeed()
        {
            // Arrange 
            User user = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> dbSetMoq = new Mock<DbSet<User>>();
            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();
            moq.Setup(m => m.User).Returns(dbSetMoq.Object);

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Create(user);

            // Assert
            moq.Verify(m => m.SaveChanges());
            dbSetMoq.Verify(m => m.Add(user));
            Assert.AreNotEqual(Guid.Empty, user.Uid);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordNotNewException))]
        public void CreateUser_WithId_ThrowsException()
        {
            // Arrange 
            User user = new User
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordNotNewException))]
        public void CreateUser_WithUid_ThrowsException()
        {
            // Arrange 
            User user = new User
            {
                Uid = Guid.NewGuid(),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Create(user);
        }

        [TestMethod]
        [ExpectedException(typeof(RecordNotNewException))]
        public void CreateUser_WithIdAndUidThrowsException()
        {
            // Arrange 
            User user = new User
            {
                Id = 2,
                Uid = Guid.NewGuid(),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };


            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Create(user);


        }

        [TestMethod]
        public void UserWithUid_Delete_Shoud_Succeed()
        {
            // Arrange 
            User user = new User
            {
                Id = 2,
                Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> userDbSetMock = GetQueryableMockDbSet(
                new List<User> {user});

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();
            
            moq.Setup(m => m.User).Returns(userDbSetMock.Object);
            moq.Setup(m => m.Set<User>()).Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Delete(user);

            userDbSetMock.Verify(m => m.Remove(user));
            moq.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        [ExpectedException(typeof(RecordNotFoundInDbException))]
        public void NotFoundUser_Delete_Should_Throw_Exception()
        {
            // Arrange 
            User user = new User
            {
                Id = 2,
                //Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> userDbSetMock = GetQueryableMockDbSet(
                new List<User>());

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            moq.Setup(m => m.User).Returns(userDbSetMock.Object);
            moq.Setup(m => m.Set<User>()).Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Delete(user);
        }

        [TestMethod]
        public void Update_Existing_User_Should_Succeed()
        {
            // Arrange 
            User user = new User
            {
                Id = 2,
                Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> userDbSetMock = GetQueryableMockDbSet(
                new List<User> { user });

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            moq.Setup(m => m.User).Returns(userDbSetMock.Object);
            moq.Setup(m => m.Set<User>()).Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Update(user);
            
            moq.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        [ExpectedException(typeof(RecordNotFoundInDbException))]
        public void Update_NotExisting_User_Should_Fail()
        {
            // Arrange 
            User user = new User
            {
                Id = 2,
                Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> userDbSetMock = GetQueryableMockDbSet(
                new List<User>());

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            moq.Setup(m => m.User).Returns(userDbSetMock.Object);
            moq.Setup(m => m.Set<User>()).Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(moq.Object);

            // Act
            repository.Update(user);
            
        }

        [TestMethod]
        public void Update_Existing_User_All_Attributes_Should_Succeed()
        {
            // Arrange 
            User userInDb = new User
            {
                Id = 2,
                Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstName",
                LastName = "LastName",
                Login = "Login",
                Password = "Password",
                Mail = "test@example.org",
                Role = UserRole.Admin
            };

            Mock<DbSet<User>> userDbSetMock = GetQueryableMockDbSet(
                new List<User> { userInDb });

            Mock<ISimplePortalEfDbContext> moq = new Mock<ISimplePortalEfDbContext>();

            moq.Setup(m => m.User).Returns(userDbSetMock.Object);
            moq.Setup(m => m.Set<User>()).Returns(userDbSetMock.Object);

            UserRepository repository = new UserRepository(moq.Object);

            User userToUpdate = new User
            {
                Id = 2,
                Uid = new Guid("57D963D7-ABFC-4DD6-89A6-93A7D15B8FC8"),
                FirstName = "FirstNameNew",
                LastName = "LastNameNew",
                Login = "LoginNew",
                Password = "PasswordNew",
                Mail = "test@example.orgnew",
                Role = UserRole.Reader
            };

            // Act
            repository.Update(userToUpdate);

            moq.Verify(m => m.SaveChanges());

            User updatedUserInDb = repository.FindRecord(userToUpdate.Uid);

            Assert.IsNotNull(updatedUserInDb);

            Assert.AreEqual(userToUpdate.FirstName, updatedUserInDb.FirstName);
            Assert.AreEqual(userToUpdate.LastName, updatedUserInDb.LastName);
            Assert.AreEqual(userToUpdate.Login, updatedUserInDb.Login);
            Assert.AreEqual(userToUpdate.Password, updatedUserInDb.Password);
            Assert.AreEqual(userToUpdate.Mail, updatedUserInDb.Mail);
            Assert.AreEqual(userToUpdate.Role, updatedUserInDb.Role);
        }

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }


    }
}
