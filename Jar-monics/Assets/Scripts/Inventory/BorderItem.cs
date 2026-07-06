using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BorderItem : MonoBehaviour
{
    private RectTransform sprite;
    [SerializeField] private Color colour = Color.red;
private void OnDrawGizmos(){
    if(sprite == null){
        sprite = GetComponent<RectTransform>();
    }
    // Gizmos.color = colour;
    // Vector3 position = transform.position;
    // Debug.Log(sprite.rect.size);
    // position.z = 0;
    // Gizmos.DrawWireCube(position, 1);

}
}
