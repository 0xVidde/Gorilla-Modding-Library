using UnityEngine;

namespace Example
{
    class MainClass : MonoBehaviour
    {
        public void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                TestFunction(
                    PrimitiveType.Cube,
                    new Vector3(1, i, 1),
                    Color.red,
                    "Example Object"
                );
            }
        }

        public GameObject TestFunction(PrimitiveType type, Vector3 pos, Color color, string Name)
        {
            GameObject obj = GameObject.CreatePrimitive(type);
            obj.transform.position = pos;
            obj.GetComponent<MeshRenderer>().material.color = color;
            obj.name = Name;

            return obj;
        }
    }
}
