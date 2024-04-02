using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectMoving : MonoBehaviour {
    private Vector3 startPoint; // ��������� �����
    private Vector3 endPoint; // �������� �����
    public float speed = 5f; // �������� ����������� �������
    public float travelDistance = 5f;
    private IEnumerator movementCoroutine;

    void Start()
    {
        startPoint = transform.position + Vector3.left * travelDistance;
        endPoint = transform.position + Vector3.right * travelDistance;

        movementCoroutine = MoveBetweenPoints();
        StartCoroutine(movementCoroutine);
    }

    IEnumerator MoveBetweenPoints()
    {
        Vector3 currentTarget = endPoint;
        while (true)
        {
            while (transform.position != currentTarget)
            {
                // ���������� ������ �� ������� ������� � ������� �����
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
                yield return null; // ��������� �������� ��������������� �� ���� ����
            }

            // ������ ����������� ��������, ����� �������� ����� �� �����
            if (currentTarget == endPoint)
                currentTarget = startPoint;
            else
                currentTarget = endPoint;

            yield return null; // ��������� �������� ��������������� �� ���� ����
        }
    }
}
