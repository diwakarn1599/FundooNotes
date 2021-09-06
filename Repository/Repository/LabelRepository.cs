using System;
using System.Collections.Generic;
using System.Text;
using Models.Models;
using Repository.Context;
using Repository.Interface;

namespace Repository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext labelContext;

        public LabelRepository(UserContext collaboratorContext)
        {
            this.labelContext = collaboratorContext;
        }
        public bool AddLabeltoNote(LabelModel labelData)
        {
            try
            {
                if(labelData!=null)
                {
                    this.labelContext.Labels.Add(labelData);
                    this.labelContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
