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

        public LabelRepository(UserContext labelContext)
        {
            this.labelContext = labelContext;
        }
        public bool AddLabeltoNote(LabelModel labelData)
        {
            try
            {
                
                var checkLabel = this.labelContext.Labels.Where(label=> label.LabelName.Equals(labelData.LabelName) && labelData.UserId == label.UserId && label.NoteId != null).FirstOrDefault();
                if (checkLabel == null)
                {
                    this.labelContext.Labels.Add(labelData);
                    this.labelContext.SaveChanges();
                    var checkLabelExists = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName) && x.NoteId==null).FirstOrDefault();
                    if (checkLabelExists == null)
                    {
                        labelData.LabelId = 0;
                        labelData.NoteId = null;
                        this.labelContext.Labels.Add(labelData);
                        this.labelContext.SaveChanges();
                    }
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
                var checkLabelExists = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName)).FirstOrDefault();
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

        public bool DeleteLabelFromNote(int labelId)
        {
            try
            {
                var getLabel = this.labelContext.Labels.Find(labelId);
                if (getLabel != null)
                {
                    this.labelContext.Labels.Remove(getLabel);
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

        public bool DeleteLabelFromUser(int userId, string labelName)
        {
            try
            {
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == userId && x.LabelName.Equals(labelName)).ToList();
                if (getAllLabels != null)
                {
                    this.labelContext.Labels.RemoveRange(getAllLabels);
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

        public bool EditLabelName(int userId, string oldLabelName, string newLabelName)
        {
            try
            {
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == userId && x.LabelName.Equals(oldLabelName)).ToList();
                if (getAllLabels.Count>0)
                {
                    getAllLabels.ForEach(i => i.LabelName = newLabelName);
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

        public List<NotesModel> GetLabelNotes(int userId,string labelName)
        {
            try
            {
                var getAllLabels = (from label in this.labelContext.Labels
                                   join notes in this.labelContext.Notes on label.NoteId equals notes.NoteId
                                    where (label.UserId == userId && label.LabelName.Equals(labelName) && notes.Trash==false)
                                   select notes).ToList();
                if (getAllLabels.Count>0)
                {
                    return getAllLabels;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<string> GetLabelofUser(int userId)
        {
            try
            {
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == userId).Select(i=>i.LabelName).Distinct().ToList();
                if (getAllLabels.Count > 0)
                {
                    return getAllLabels;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
