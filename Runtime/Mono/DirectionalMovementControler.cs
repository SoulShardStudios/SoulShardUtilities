using UnityEngine;

namespace SoulShard.Utils
{
    /// <summary>
    /// Simple WASD or arrow keys based movement.
    /// This is a simple implementation only really designed to test stuff out.
    /// </summary>
    public class DirectionalMovementControler : MonoBehaviour
    {
        [SerializeField]
        float speed;

        private void Update()
        {
            transform.position += speed * (Vector3)GetMovementVector() * Time.deltaTime;
        }

        bool GetKeys(KeyCode[] codes)
        {
            foreach (KeyCode c in codes)
                if (Input.GetKey(c))
                    return true;
            return false;
        }

        Vector2 GetMovementVector()
        {
            Vector2 res = Vector2.zero;
            if (GetKeys(new KeyCode[2] { KeyCode.LeftArrow, KeyCode.D }))
                res.x -= 1;
            if (GetKeys(new KeyCode[2] { KeyCode.RightArrow, KeyCode.A }))
                res.x += 1;
            if (GetKeys(new KeyCode[2] { KeyCode.DownArrow, KeyCode.S }))
                res.y -= 1;
            if (GetKeys(new KeyCode[2] { KeyCode.UpArrow, KeyCode.W }))
                res.y += 1;
            return res;
        }
    }
}
