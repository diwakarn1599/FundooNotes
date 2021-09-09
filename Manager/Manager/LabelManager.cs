// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using global::Manager.Interface;
    using Models.Models;
    using Repository.Interface;

    /// <summary>
    /// label manager class
    /// </summary>
    /// <seealso cref="Manager.Interface.ILabelManager" />
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ILabelRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the label to note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label added or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
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

        /// <summary>
        /// Adds the label to user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label added or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
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

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// boolean of label deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
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

        /// <summary>
        /// Deletes the label from user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        /// boolean of label deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
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

        /// <summary>
        /// Edits the name of the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="oldLabelName">Old name of the label.</param>
        /// <param name="newLabelName">New name of the label.</param>
        /// <returns>
        /// boolean of label edited or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool EditLabelName(int userId, string oldLabelName, string newLabelName)
        {
            try
            {
                return this.repository.EditLabelName(userId, oldLabelName, newLabelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        /// list of label notes
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public List<NotesModel> GetLabelNotes(int userId, string labelName)
        {
            try
            {
                return this.repository.GetLabelNotes(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user labels
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
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
