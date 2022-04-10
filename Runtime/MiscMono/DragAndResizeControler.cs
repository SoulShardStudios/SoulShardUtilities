using UnityEngine;
namespace SoulShard.Utils
{
    /// <summary>
    /// A camera based controler that allows zooming and dragging of the given camera to control movement.
    /// </summary>
    public class DragAndResizeControler : MonoBehaviour
    {
        #region Management
        public static DragAndResizeControler Instance { get; private set; }
        /// <summary>
        /// The camera to process the movement of based on the mouse position.
        /// </summary>
        [SerializeField] Camera _camera;
        void Update()
        {
            ProcessZoom();
            ProcessMovement();
        }
        #endregion
        #region Zoom
        [SerializeField] float _zoomSpeed, _minZoom, _maxZoom;
        void ProcessZoom()
        {
            float scrollDelta = Input.mouseScrollDelta.y * -1;
            if (scrollDelta != 0)
            {
                float change = scrollDelta * _zoomSpeed;
                float _newSize = Mathf.Clamp(change + _camera.orthographicSize, _minZoom, _maxZoom);
                _camera.orthographicSize = _newSize;
            }
        }
        #endregion
        #region Movement
        Vector2 _dragOrigin;
        [SerializeField] int _dragButton;
        void ProcessMovement()
        {
            Vector2 difference = new Vector2();
            Vector2 s2wp = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(_dragButton))
                _dragOrigin = s2wp;

            if (Input.GetMouseButton(_dragButton))
                difference = _dragOrigin - s2wp;

            _camera.transform.position += (Vector3)difference;
        }
        #endregion
    }
}
