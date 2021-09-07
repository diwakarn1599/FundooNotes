using System;
using System.Collections.Generic;
using System.Text;
using Manager.Interface;
using Models.Models;
using Repository.Interface;

namespace Manager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;

        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }
        public bool AddLabeltoNote(LabelModel labelData)
        {
            try
            {
                return this.repository.AddLabeltoNote(labelData);
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
                return this.repository.AddLabeltoUser(labelData);
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
                return this.repository.DeleteLabelFromNote(labelId);
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
                return this.repository.DeleteLabelFromUser(userId, labelName);
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
                return this.repository.EditLabelName(userId,oldLabelName,newLabelName);
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
                return this.repository.GetLabelNotes(userId,labelName);
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
                return this.repository.GetLabelofUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
