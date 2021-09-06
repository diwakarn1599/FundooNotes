using System;
using System.Collections.Generic;
using System.Linq;
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
                var checkLabel = this.labelContext.Labels.Where(x => x.NoteId == labelData.NoteId && x.LabelName.Equals(labelData.LabelName)).SingleOrDefault();
                if (checkLabel == null)
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

        public bool AddLabeltoUser(LabelModel labelData)
        {
            try
            {
                var checkLabelExists = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName)).SingleOrDefault();
                if (checkLabelExists == null)
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
