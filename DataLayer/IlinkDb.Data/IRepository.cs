using System;
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

        Note NoteGet(Story story, long noteId);
        Note NoteSave(Story story, Note newItem);
        bool NoteDelete(Note note);
        IQueryable<Note> NoteList(Story story);

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

        Story StoryGet(long projectId, long storyId);
        Story StorySave(Story story);
        bool StoryDelete(Story story);
        IQueryable<Story> StoryList(long projectId);
        IQueryable<Story> StoryListForLabel(long projectId, string label);

        #endregion

        #region Task

        Task TaskGet(Story story, long id);
        Task TaskSave(Story story, Task task);
        bool TaskDelete(Task task);
        IQueryable<Task> TaskList(Story story);

        #endregion
    }
}
