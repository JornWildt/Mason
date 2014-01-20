using Mason.IssueTracker.Server.Domain.Exceptions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Mason.IssueTracker.Server.Domain.NHibernate
{
  public class NHibernateGenericRepository<T, IdT> where T : class
  {
    protected ISession Session
    {
      get
      {
        return SessionManager.CurrentSession;
      }
    }


    #region -- Add ------------------------------------------------------------

    public virtual void Add(T entity)
    {
      Session.Save(entity);
    }

    #endregion


    #region -- Get by ID ------------------------------------------------------

    public virtual T Get(IdT id)
    {
      T entity = GetNullable(id);
      if (entity == null)
        throw new MissingResourceException(string.Format("Unknown id {0} for {1}", id, typeof(T)));
      return entity;
    }

    public virtual T GetNullable(IdT id)
    {
      return Session.Get<T>(id);
    }

    #endregion


    #region -- Find and count all ---------------------------------------------

    public virtual IEnumerable<T> FindAll()
    {
      return Find();
    }


    public virtual long CountAll()
    {
      return Count();
    }

    #endregion


    //#region -- Linq -----------------------------------------------------------

    public virtual IQueryable<T> Query()
    {
      return Session.Query<T>();
    }

    //#endregion


    #region -- Query by queryable ---------------------------------------------


    public virtual IEnumerable<T> Find(IQueryable<T> query)
    {
      return query;
    }


    public virtual T GetNullable(IQueryable<T> query)
    {
      IEnumerable<T> result = Find(query);
      T first = result.FirstOrDefault();
      return first;
    }

    
    public virtual T Get(IQueryable<T> query)
    {
      T result = GetNullable(query);
      if (result == null)
        throw new MissingResourceException(string.Format("No data found for {0}", this.GetType()));
      return result;
    }

    #endregion


    #region -- Query by criterion ---------------------------------------------

    public virtual IEnumerable<T> Find(params ICriterion[] criterion)
    {
      ICriteria criteria = Session.CreateCriteria(typeof(T));

      foreach (ICriterion criterium in criterion)
      {
        criteria.Add(criterium);
      }

      return criteria.List<T>();
    }


    public virtual T GetNullable(params ICriterion[] criterion)
    {
      IEnumerable<T> result = Find(criterion);
      if (result.Count() == 0)
        return null;
      if (result.Count() > 1)
        throw new ApplicationException(string.Format("Got {0} records but expected only 1 for {1}", result.Count(), this.GetType()));
      return result.ElementAt(0);
    }


    public virtual T Get(params ICriterion[] criterion)
    {
      T result = GetNullable(criterion);
      if (result == null)
        throw new MissingResourceException(string.Format("No data found for {0}", typeof(T)));
      return result;
    }


    public virtual long Count(params ICriterion[] criterion)
    {
      ICriteria criteria = Session.CreateCriteria(typeof(T));

      foreach (ICriterion criterium in criterion)
      {
        criteria.Add(criterium);
      }

      criteria.SetProjection(Projections.RowCount());
      IList result = criteria.List();
      return (int)result[0];
    }

    #endregion


    #region -- Delete ---------------------------------------------------------

    public virtual void Delete(T entity)
    {
      Session.Delete(entity);
    }

    public virtual void DeleteAll()
    {
      foreach (T entity in FindAll())
        Delete(entity);
    }

    #endregion


    //#region -- Update ---------------------------------------------------------

    //public virtual void Update(T entity)
    //{
    //  Session.Update(entity);
    //}

    //#endregion
  }
}
