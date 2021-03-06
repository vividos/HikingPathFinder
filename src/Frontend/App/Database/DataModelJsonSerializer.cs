﻿using HikingPathFinder.Model;
using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HikingPathFinder.App.Database
{
    /// <summary>
    /// Json serializer for some custom types that are used in the data model
    /// </summary>
    internal class DataModelJsonSerializer : IBlobSerializer
    {
        /// <summary>
        /// List of supported types
        /// </summary>
        private readonly List<Type> supportedTypes = new List<Type>
        {
            typeof(MapRectangle),
            typeof(MapPoint),
            typeof(Tour),
            typeof(List<PhotoRef>),
            typeof(PlanTourParameters),
        };

        /// <summary>
        /// Returns if the given .NET type can be deserialized by this serializer
        /// </summary>
        /// <param name="type">.NET type to check</param>
        /// <returns>true when deserialisation can be done, false when not</returns>
        public bool CanDeserialize(Type type)
        {
            return this.supportedTypes.Contains(type);
        }

        /// <summary>
        /// Deserializes a given type from provided blob data. Throws an exception on unknown
        /// types.
        /// </summary>
        /// <param name="data">data to use</param>
        /// <param name="type">type to deserialize to</param>
        /// <returns>deserialized object</returns>
        public object Deserialize(byte[] data, Type type)
        {
            string json = Encoding.UTF8.GetString(data, 0, data.Length);

            if (this.CanDeserialize(type))
            {
                return JsonConvert.DeserializeObject(json, type);
            }

            string message = string.Format("deserializing of .NET type {0} not supported", type.FullName);
            throw new NotImplementedException(message);
        }

        /// <summary>
        /// Serializes a given object to a binary stream
        /// </summary>
        /// <typeparam name="T">type of object to serialize</typeparam>
        /// <param name="obj">object instance to serialize</param>
        /// <returns>blob data of serialized object</returns>
        public byte[] Serialize<T>(T obj)
        {
            Debug.Assert(
                this.CanDeserialize(obj.GetType()),
                "can only deserialize one of the supported .NET types");

            string json = JsonConvert.SerializeObject(obj);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}
