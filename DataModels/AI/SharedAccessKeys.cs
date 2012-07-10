﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Skill.DataModels.AI
{
    /// <summary>
    /// Defines Acceess keys to share between BehaviorTrees
    /// </summary>
    public class SharedAccessKeys : IXElement
    {
        #region Properties
        /// <summary> Collection of AccessKeys  </summary>
        public AccessKey[] Keys { get; set; }
        /// <summary> Name of file </summary>
        public string Name { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create new instance of SharedAccessKeys
        /// </summary>
        public SharedAccessKeys()
        {
            this.Name = "AccessKeys";
        }
        #endregion

        #region Save
        public XElement ToXElement()
        {
            XElement accessKeys = new XElement("AccessKeys");
            accessKeys.SetAttributeValue("Name", Name);

            if (this.Keys != null)
            {
                accessKeys.SetAttributeValue("Count", Keys.Length);
                foreach (var item in Keys)
                {
                    XElement n = item.ToXElement();
                    accessKeys.Add(n);
                }
            }
            return accessKeys;
        }
        #endregion

        #region Load
        public void Load(XElement e)
        {
            int count = e.GetAttributeValueAsInt("Count", 0);
            this.Name = e.GetAttributeValueAsString("Name", this.Name);
            Keys = new AccessKey[count];
            int index = 0;
            foreach (var item in e.Elements())
            {
                AccessKey ak = CreateAccessKeyFrom(item);
                if (ak != null)
                {
                    ak.Load(item);
                    Keys[index++] = ak;
                }
            }

        }

        private static AccessKey CreateAccessKeyFrom(XElement node)
        {
            AccessKey result = null;
            AccessKeyType accessKeyType = AccessKeyType.TimeLimit;
            bool isCorrect = false;
            try
            {
                accessKeyType = (AccessKeyType)Enum.Parse(typeof(AccessKeyType), node.Name.ToString(), false);
                isCorrect = true;
            }
            catch (Exception)
            {
                isCorrect = false;
            }
            if (isCorrect)
            {
                switch (accessKeyType)
                {
                    case AccessKeyType.CounterLimit:
                        result = new CounterLimitAccessKey();
                        break;
                    case AccessKeyType.TimeLimit:
                        result = new TimeLimitAccessKey();
                        break;
                }
            }
            return result;
        }
        #endregion
    }
}