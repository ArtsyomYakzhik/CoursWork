using CourseWork.Models.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models.DBInteractions
{
    public abstract class Interaction
    {
        protected DBObject dBContext;

        public Interaction()
        {
            dBContext = new DBObject();
        }

        protected void SaveChanges()
        {
            dBContext.SaveChanges();
        }
    }
}