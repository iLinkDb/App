﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Entity;

namespace IlinkDb.Data
{
    public interface IRepository
    {
        void Initialize();
        //T Get<T>(long id) where T : EntityBase;
        //T Save<T>(T t) where T : EntityBase;
        //bool Delete<T>(T t) where T : EntityBase;
        //IQueryable<T> List<T>() where T : EntityBase;

        #region Link

        Link LinkGet(long id);
        Link LinkSave(Link link);
        bool LinkDelete(Link link);
        IQueryable<Link> LinkList();

        #endregion

        #region Tenant

        Tenant TenantGet(long id);
        Tenant TenantSave(Tenant tenant);
        bool TenantDelete(Tenant tenant);
        IQueryable<Tenant> TenantList();

        #endregion

        #region Note

        Note NoteGet(long id);
        Note NoteSave(Note note);
        bool NoteDelete(Note note);
        IQueryable<Note> NoteList();
        Note Add(Story story, Note note);

        #endregion

        #region User

        //TODO Put User setting back in IRepository IF.
        //User UserGet(long id);
        //User UserSave(User user);
        //bool UserDelete(User user);
        //IQueryable<User> UserList();

        #endregion

        #region Project

        Project ProjectGet(long id);
        Project ProjectSave(Project project);
        bool ProjectDelete(Project project);
        IQueryable<Project> ProjectList();

        #endregion

        #region Story

        Story StoryGet(long id);
        Story StorySave(Story story);
        bool StoryDelete(Story story);
        IQueryable<Story> StoryList(long projectId);
        IQueryable<Story> StoryListForLabel(long projectId, string label);
        Story Add(long projectId, Story story);

        #endregion

        #region Task

        Task Get(long id);
        Task Save(Task task);
        bool Delete(Task task);
        IQueryable<Task> List(Story story);
        Task Add(Story story, Task task);

        #endregion
    }
}
