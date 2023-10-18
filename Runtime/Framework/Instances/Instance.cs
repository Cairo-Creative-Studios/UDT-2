using System;
using UnityEngine;
using System.Collections.Generic;
using UDT.Scriptables.Events;
using NaughtyAttributes;
using UDT.StateMachines;
using UDT.PrefabTables;
using log4net.Core;
using System.Linq;

namespace UDT.Instances
{
    [Serializable]
    public sealed class Instance : IStateMachine
    {
        private static DropdownList<GameObject> cachedPrefabList;
        public static DropdownList<GameObject> PrefabDropdownList
        {
            get
            {
                if (cachedPrefabList == null)
                {
                    DropdownList<UnityEngine.GameObject> prefabs = new()
                    {
                        { "None", null }
                    };

                    foreach (var prefab in Resources.LoadAll<UnityEngine.GameObject>("Objects"))
                    {
                        prefabs.Add(prefab.name, prefab);
                    }

                    cachedPrefabList = prefabs;

                    return prefabs;
                }
                else
                    return cachedPrefabList;
            }
        }

        public Prefab Prefab;

        public GameObject GameObject;
        GameObject IStateMachine.gameObject => GameObject;
        public Transform transform;

        private static int UIDCounter = 0;
        private int _UID;
        public int UID { get => _UID; private set => _UID = value; }
        private static List<Instance> instances = new();
        public static List<Instance> Instances { get => instances; }

        private enum MotionController
        {
            Rigidbody,
            CharacterController,
            Basic
        }
        private MotionController motionController = MotionController.Rigidbody;
        private Rigidbody rigidbody;
        private CharacterController characterController;
        private Vector3 _speed;
        private Vector3 _acceleration;
        private Vector3 _direction;


        public float x
        {
            get => transform.position.x;
            set => transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        public float y
        {
            get => transform.position.y;
            set => transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        public float z
        {
            get => transform.position.z;
            set => transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public float roll
        {
            get => transform.rotation.eulerAngles.x;
            set => transform.rotation = Quaternion.Euler(value, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        public float pitch
        {
            get => transform.rotation.eulerAngles.y;
            set => transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, value, transform.rotation.eulerAngles.z);
        }
        public float yaw
        {
            get => transform.rotation.eulerAngles.z;
            set => transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, value);
        }
        public Vector3 rotation
        {
            get => transform.rotation.eulerAngles;
            set => transform.rotation = Quaternion.Euler(value);
        }
        public float scaleX
        {
            get => transform.localScale.x;
            set => transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        }
        public float scaleY
        {
            get => transform.localScale.y;
            set => transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
        }
        public float scaleZ
        {
            get => transform.localScale.z;
            set => transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
        }
        public Vector3 scale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
        public float xSpeed
        {
            get
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        return rigidbody.velocity.x;
                    case MotionController.CharacterController:
                        return characterController.velocity.x;
                    default:
                        return _speed.x;
                }
            }
            set
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        rigidbody.velocity = new Vector3(value, rigidbody.velocity.y, rigidbody.velocity.z);
                        break;
                    case MotionController.CharacterController:
                        characterController.Move(new Vector3(value, characterController.velocity.y, characterController.velocity.z));
                        break;
                    default:
                        _speed.x = value;
                        break;
                }
            }
        }
        public float ySpeed
        {
            get
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        return rigidbody.velocity.y;
                    case MotionController.CharacterController:
                        return characterController.velocity.y;
                    default:
                        return _speed.y;
                }
            }
            set
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        rigidbody.velocity = new Vector3(rigidbody.velocity.x, value, rigidbody.velocity.z);
                        break;
                    case MotionController.CharacterController:
                        characterController.Move(new Vector3(characterController.velocity.x, value, characterController.velocity.z));
                        break;
                    default:
                        _speed.y = value;
                        break;
                }
            }
        }
        public float zSpeed
        {
            get
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        return rigidbody.velocity.z;
                    case MotionController.CharacterController:
                        return characterController.velocity.z;
                    default:
                        return _speed.z;
                }
            }
            set
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, value);
                        break;
                    case MotionController.CharacterController:
                        characterController.Move(new Vector3(characterController.velocity.x, characterController.velocity.y, value));
                        break;
                    default:
                        _speed.z = value;
                        break;
                }
            }
        }
        public Vector3 speedVector
        {
            get
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        return rigidbody.velocity;
                    case MotionController.CharacterController:
                        return characterController.velocity;
                    default:
                        return _speed;
                }
            }
            set
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        rigidbody.velocity = value;
                        break;
                    case MotionController.CharacterController:
                        characterController.Move(value);
                        break;
                    default:
                        _speed = value;
                        break;
                }
            }
        }
        public float speed
        {
            get
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        return rigidbody.velocity.magnitude;
                    case MotionController.CharacterController:
                        return characterController.velocity.magnitude;
                    default:
                        return _speed.magnitude;
                }
            }
            set
            {
                switch (motionController)
                {
                    case MotionController.Rigidbody:
                        rigidbody.velocity = rigidbody.velocity.normalized * value;
                        break;
                    case MotionController.CharacterController:
                        characterController.Move(characterController.velocity.normalized * value);
                        break;
                    default:
                        _speed = _speed.normalized * value;
                        break;
                }
            }
        }

        public float xAcceleration
        {
            get
            {
                return _acceleration.x;
            }
            set
            {
                _acceleration.x = value;
            }
        }
        public float yAcceleration
        {
            get
            {
                return _acceleration.y;
            }
            set
            {
                _acceleration.y = value;
            }
        }
        public float zAcceleration
        {
            get
            {
                return _acceleration.z;
            }
            set
            {
                _acceleration.z = value;
            }
        }
        public float acceleration
        {
            get
            {
                return _acceleration.magnitude;
            }
            set
            {
                _acceleration = _acceleration.normalized * value;
            }
        }
        public float xDirection
        {
            get
            {
                return Vector3.Angle(_speed, Vector3.right);
            }
            set
            {
                _speed = Quaternion.Euler(value, 0, 0) * Vector3.right * speed;
            }
        }
        public float yDirection
        {
            get
            {
                return Vector3.Angle(_speed, Vector3.up);
            }
            set
            {
                _speed = Quaternion.Euler(0, value, 0) * Vector3.up * speed;
            }
        }
        public float zDirection
        {
            get
            {
                return Vector3.Angle(_speed, Vector3.forward);
            }
            set
            {
                _speed = Quaternion.Euler(0, 0, value) * Vector3.forward * speed;
            }
        }
        public float direction
        {
            get
            {
                return Vector3.Angle(_speed, Vector3.forward);
            }
            set
            {
                _speed = Quaternion.Euler(0, 0, value) * Vector3.forward * speed;
            }
        }


        public Instance(string name, Vector3 position = default, Quaternion rotation = default)
        {
            GameObject = new GameObject(name);
            SetupInstance();
            transform.position = position;
            transform.rotation = rotation;
        }

        public Instance(Prefab prefab)
        {
            this.GameObject = GameObject.Instantiate(prefab.GameObject);
            PrefabPools.AddToPool(prefab, this);
            GameObject.name = prefab.GameObject.name + PrefabPools.PoolSize(prefab.GameObject);
            SetupInstance();
        }

        public Instance(GameObject gameObject) : this(new Prefab(gameObject)) { }

        private void SetupInstance()
        {
            transform = GameObject.transform;

            rigidbody = GameObject.GetComponent<Rigidbody>();
            characterController = GameObject.GetComponent<CharacterController>();

            if (rigidbody != null)
            {
                motionController = MotionController.Rigidbody;
            }
            else if (characterController != null)
            {
                motionController = MotionController.CharacterController;
            }
            else
            {
                motionController = MotionController.Basic;
            }

            UID = UIDCounter++;

            instances.Add(this);
            OnInstanceCreated.Invoke(this);
        }

        /// <summary>
        /// Gets the instance of this game object, or creates one if it doesn't exist.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Instance GetAsInstance(GameObject gameObject)
        {
            var existing = instances.Find(instance => instance.GameObject == gameObject);
            if(existing != null)
            {
                return existing;
            }
            else
            {
                var newInstance = new Instance(gameObject);
                return newInstance;
            }
        }

        public static Instance GetInstanceByUID(int UID)
        {
            return Instances.FirstOrDefault(x => x.UID == UID);
        }

        public T AddComponent<T>() where T : Component
        {
            Type type = typeof(T);

            if (type == typeof(Rigidbody))
            {
                motionController = MotionController.Rigidbody;
            }
            else if (type == typeof(CharacterController))
            {
                motionController = MotionController.CharacterController;
            }
            else
            {
                motionController = MotionController.Basic;
            }

            return GameObject.AddComponent<T>();
        }

        public T AddComponent<T>(T component) where T : Component
        {
            return AddComponent<T>();
        }

        public void RemoveComponent<T>() where T : Component
        {
            Component.Destroy(GameObject.GetComponent<T>());
        }

        public void RemoveComponent<T>(T component) where T : Component
        {
            Component.Destroy(component);
        }

        public void Destroy()
        {
            OnInstanceDestroyed.Invoke(this);
            instances.Remove(this);
            GameObject.Destroy(GameObject);
        }

        public static void DestroyInstance(GameObject prefab, int IID)
        {
            PrefabPools.RemoveInstanceByIID(prefab, IID);
        }
    }
}