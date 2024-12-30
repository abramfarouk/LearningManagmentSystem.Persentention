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
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";

        }
        public static class AuthenicationRouting
        {
            public const string Prefix = Rule + "Authenication/";
            public const string SignIn = Prefix + "Sign-In";
            public const string IsValidToken = Prefix + "IsValid-Token";
            public const string RefreshToken = Prefix + "Refresh-Token";
            public const string ConfirmEmail = Prefix + "Confirm-Email";
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

        public static class LessonRouting
        {
            public const string Prefix = Rule + "Lesson/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";


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

        public static class ModuleRouting
        {
            public const string Prefix = Rule + "Module/";
            public const string List = Prefix + "List";
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
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";
        }

        public static class EmailRouting
        {
            public const string Prefix = Rule + "Email/";
            public const string List = Prefix + "List";
            public const string GetById = Prefix + "GetById/{id:int}";
            public const string GetByName = Prefix + "GetByName/{name:alpha}";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete";

        }

    }
}
