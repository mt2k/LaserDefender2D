using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> wayPoints;
    WaveConfigs waveConfig;
    int waypointIndex = 0;

    void Start()
    {
        //transform.position = wayPoints[waypointIndex].position;
        wayPoints = waveConfig.GetWaypoints();
        transform.position = wayPoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfigs waveConfig) {
        /*
         * this.waveConfig ==> khi dùng this nếu trường hợp hai biến được khai báo trùng tên nhau 
         * (VD: 2 biến waveConfig 1 biến được khai báo ở ngoài function() và 1 biến ở trong
         * Trong trường hợp này thì this sẽ được khai báo và sử dụng biến ở ngoài function() chứ không phải ở trong
         * Có thể kiểm tra bằng cách thay biến wayConfig ở trong function() thành 1 cái tên khác 
         * (VD: waveConfigss ==> thì khi khai báo this sẽ không xuất hiện biến waveConfigss
         */
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                movementThisFrame);
            if (targetPosition == transform.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);            
        }
    }
}
