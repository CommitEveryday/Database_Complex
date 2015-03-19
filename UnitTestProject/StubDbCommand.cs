using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    class StubDbCommand : DbCommand
    {
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override string CommandText
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int CommandTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override System.Data.CommandType CommandType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbConnection DbConnection
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbDataReader ExecuteDbDataReader(System.Data.CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override System.Data.UpdateRowSource UpdatedRowSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
