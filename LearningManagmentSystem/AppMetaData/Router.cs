namespace LearningManagmentSystem.AppMetaData
{
    public static class Router
    {
        public const string Root = "Api";
        public const string Version = "v1";
        public const string Rule = Root + "/" + Version + "/";

        public static class AssignmentRouting
        {
            public const string Prefix = Rule + "Assignment/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";

        }
        public static class AuthenicationRouting
        {
            public const string Prefix = Rule + "Authenication/";
            public const string SignIn = Prefix + "Sign-In";
            public const string ResetPassword = Prefix + "Reset-Password";
            public const string ConfirmResetPassword = Prefix + "Confirm-Reset-Password";
            public const string SendResetPasswordCode = Prefix + "Send-Reset-Password-Code";
            public const string IsValidToken = Prefix + "IsValid-Token";
            public const string RefreshToken = Prefix + "Refresh-Token";
            public const string ConfirmEmail = Prefix + "Confirm-Email";
        }

        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization/";
            public const string AddRole = Prefix + "Add-Role";
            public const string UpdatedRole = Prefix + "Update-Role";
            public const string DeleteRole = Prefix + "Delete-Role";
            public const string GetRoles = Prefix + "Get-Roles";
            public const string GetRoleById = Prefix + "GetById/{GetById:int}";
        }
        public static class CertificationRouting
        {
            public const string Prefix = Rule + "Certification/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }
        public static class ForumRouting
        {
            public const string Prefix = Rule + "Forum/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }
        public static class ForumPostRouting
        {
            public const string Prefix = Rule + "ForumPost/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";

        }
        public static class SubmissionRouting
        {
            public const string Prefix = Rule + "Submissions/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }

        public static class CourseRouting
        {
            public const string Prefix = Rule + "Course/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }
        public static class GradeRouting
        {
            public const string Prefix = Rule + "Grade/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }

        public static class LessonRouting
        {
            public const string Prefix = Rule + "Lesson/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Paginated = Prefix + "Paginated";
            public const string Delete = Prefix + "Delete";


        }


        public static class ModuleRouting
        {
            public const string Prefix = Rule + "Module/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }

        public static class NotificationRouting
        {
            public const string Prefix = Rule + "Notification/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "Paginated";
            public const string Create = Prefix + "Create";
            public const string GetById = Prefix + "GetById/{id:int}";

            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }

        public static class EmailRouting
        {
            public const string Prefix = Rule + "Email/";
            public const string SendEmail = Prefix + "Send-Email";


        }
        public static class UserRouting
        {
            public const string Prefix = Rule + "User/";
            public const string List = Prefix + "List";
            public const string Paginated = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
            public const string ChangePassword = Prefix + "Change-Password";
            public const string AddStudent = Prefix + "Add-Student";
            public const string AddTeacher = Prefix + "Add-Teacher";
            public const string AddAdmin = Prefix + "Add-Admin";
        }
        public static class EnrollmentRouting
        {
            public const string Prefix = Rule + "Enrollment/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";

        }



    }
}