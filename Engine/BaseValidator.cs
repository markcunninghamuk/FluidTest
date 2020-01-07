using MarkTek.Fluent.Testing.RecordGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marktek.Fluent.Testing.Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValidator<TID,T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract List<ISpecificationValidator<T>> GetValidators();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetRecord(TID id);            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Validate(TID id)
        {
            var validators = GetValidators();
            var item = GetRecord(id);
            foreach (var val in validators)
            {
                val.Validate(item);
            }
        }
            //var o = new Order();

            ////Var 0 = repo.GetOrder();// IMAGINE

            //var x = GetValidators();
            //x.ForEach(vv => vv.Validate(o));

            //var res = File.ReadAllText("C:\\Test\\test.txt");

            //if (!res.Contains("Creating Order"))
            //{
            //    throw new System.Exception("Could not find the expected string");
            //}
        }
    }

