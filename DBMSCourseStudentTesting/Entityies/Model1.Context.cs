﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBMSCourseStudentTesting.Entityies
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StudentTestingEntities1 : DbContext
    {
        public StudentTestingEntities1()
            : base(Properties.Settings.Default.connectStudentTestingEntities)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AcademicGroup> AcademicGroup { get; set; }
        public virtual DbSet<AcademicSubject> AcademicSubject { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Examination> Examination { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<GroupTesting> GroupTesting { get; set; }
        public virtual DbSet<Protocol> Protocol { get; set; }
    
        [DbFunction("StudentTestingEntities1", "GetCurrentTest")]
        public virtual IQueryable<GetCurrentTest_Result> GetCurrentTest()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetCurrentTest_Result>("[StudentTestingEntities1].[GetCurrentTest]()");
        }
    
        public virtual ObjectResult<string> GetRole(string login)
        {
            var loginParameter = login != null ?
                new ObjectParameter("login", login) :
                new ObjectParameter("login", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetRole", loginParameter);
        }
    
        public virtual int addTeacher(string surname, string name, string patronymic, string login, string password)
        {
            var surnameParameter = surname != null ?
                new ObjectParameter("Surname", surname) :
                new ObjectParameter("Surname", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var patronymicParameter = patronymic != null ?
                new ObjectParameter("Patronymic", patronymic) :
                new ObjectParameter("Patronymic", typeof(string));
    
            var loginParameter = login != null ?
                new ObjectParameter("Login", login) :
                new ObjectParameter("Login", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addTeacher", surnameParameter, nameParameter, patronymicParameter, loginParameter, passwordParameter);
        }
    
        public virtual ObjectResult<GetAcSubjects_Result> GetAcSubjects(Nullable<int> idTeacher)
        {
            var idTeacherParameter = idTeacher.HasValue ?
                new ObjectParameter("idTeacher", idTeacher) :
                new ObjectParameter("idTeacher", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAcSubjects_Result>("GetAcSubjects", idTeacherParameter);
        }
    
        public virtual int AddAcSubjects(Nullable<int> idTeacher, Nullable<int> idSubject)
        {
            var idTeacherParameter = idTeacher.HasValue ?
                new ObjectParameter("idTeacher", idTeacher) :
                new ObjectParameter("idTeacher", typeof(int));
    
            var idSubjectParameter = idSubject.HasValue ?
                new ObjectParameter("idSubject", idSubject) :
                new ObjectParameter("idSubject", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddAcSubjects", idTeacherParameter, idSubjectParameter);
        }
    
        public virtual int DeleteAcSubjects(Nullable<int> idTeacher, Nullable<int> idSubject)
        {
            var idTeacherParameter = idTeacher.HasValue ?
                new ObjectParameter("idTeacher", idTeacher) :
                new ObjectParameter("idTeacher", typeof(int));
    
            var idSubjectParameter = idSubject.HasValue ?
                new ObjectParameter("idSubject", idSubject) :
                new ObjectParameter("idSubject", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteAcSubjects", idTeacherParameter, idSubjectParameter);
        }
    
        public virtual int addStudent(string surname, string name, string patronymic, Nullable<int> idGroup, string login, string password)
        {
            var surnameParameter = surname != null ?
                new ObjectParameter("Surname", surname) :
                new ObjectParameter("Surname", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var patronymicParameter = patronymic != null ?
                new ObjectParameter("Patronymic", patronymic) :
                new ObjectParameter("Patronymic", typeof(string));
    
            var idGroupParameter = idGroup.HasValue ?
                new ObjectParameter("idGroup", idGroup) :
                new ObjectParameter("idGroup", typeof(int));
    
            var loginParameter = login != null ?
                new ObjectParameter("Login", login) :
                new ObjectParameter("Login", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addStudent", surnameParameter, nameParameter, patronymicParameter, idGroupParameter, loginParameter, passwordParameter);
        }
    
        public virtual ObjectResult<string> GetLoginT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetLoginT");
        }
    
        public virtual int AddExam(Nullable<int> idTest, Nullable<int> idStudent, Nullable<short> mark)
        {
            var idTestParameter = idTest.HasValue ?
                new ObjectParameter("idTest", idTest) :
                new ObjectParameter("idTest", typeof(int));
    
            var idStudentParameter = idStudent.HasValue ?
                new ObjectParameter("idStudent", idStudent) :
                new ObjectParameter("idStudent", typeof(int));
    
            var markParameter = mark.HasValue ?
                new ObjectParameter("Mark", mark) :
                new ObjectParameter("Mark", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddExam", idTestParameter, idStudentParameter, markParameter);
        }
    }
}
