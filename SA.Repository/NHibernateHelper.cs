using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using SA.Repository.Domain;

namespace SA.Repository
{
    public sealed class NHibernateHelper
    {
        private static readonly ISessionFactory _sessionFactory;
        private static ISession _session;
        private const string _currentSessionKey = "nhibernate.current_session";

        static NHibernateHelper()
        {
            Configuration cfg = new Configuration()
                .Configure()
                .AddAssembly("SA.Repository");

            _sessionFactory = cfg.BuildSessionFactory();
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            if ((_session == null) || ((_session != null) && (!_session.IsOpen)))
                _session = _sessionFactory.OpenSession();
            return _session;
        }

        public static void CloseSession()
        {
            if (_session == null)
                return;

            _session.Close();
            _session = null;
        }

    }
}
