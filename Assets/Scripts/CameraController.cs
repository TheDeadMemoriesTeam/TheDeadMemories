using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public PlayerController target;

    private float targetHeight = 2.5f;
    private float distance = 3.5f;	// distance au perso

    private float maxDistance = 10f;
    private float minDistance = 3f;
	
	private float speed;	// Vitesse player

    private int yMinLimit = -120;
    private int yMaxLimit = 120;
 
    private int zoomRate = 40;
	
	// Smooth sur les zooms et rotations
    private float rotationAttenuation = 3f;
    private float zoomAttenuation = 5f;

    private float x = 0f;
    private float y = 0f;

    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    void Start ()
	{
		speed = target.speed;
		
		// Angles de la caméra
        Vector3 angles = transform.eulerAngles;

        x = angles.x;
        y = angles.y;
		
		// Init des distances
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
    }

   
    void LateUpdate ()
	{
        // Mouvements de rotation
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float targetRotationAngle = target.transform.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationAttenuation * Time.deltaTime);
        }
		
		// Affecte l'une des bornes en fonction de l'angle sur y
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // Transformation pour la caméra (rotation)
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // Calcul de la distance de la caméra entre les bornes min et max distance (selon le scroll souris)
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        // Calcul de la position de la caméra
        Vector3 position = target.transform.position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));
		
		// Vérification des colisions de la caméra avec un élément du décor
        RaycastHit collisionElement;
        Vector3 trueTargetPosition = new Vector3(target.transform.position.x, target.transform.position.y + targetHeight, target.transform.position.z);
		
		// S'il y a collision avec un élément du décor on corrige la position caméra avec le distance correcte
        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionElement))
        {
            position = collisionElement.point;
            correctedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }

		// Si la distance n'est pas correcte ou que la distance correcte est supérieure à la distance actuelle
		// On déplace la caméra à la distance actuelle à la distance correcte, dans tous les cas on se place à la distance correcte
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomAttenuation) : correctedDistance;
		
        // Recalcule la position de la caméra avec la distance correcte
        position = target.transform.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight, 0));

		// Applique la rotation et position de la caméra
        transform.rotation = rotation;
        transform.position = position;
    }

 

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}