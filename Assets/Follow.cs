using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player; // ตำแหน่งของตัวผู้เล่นที่ต้องการให้กล้องติดตาม
    public float smoothNess = 2f; // ความนุ่มนวลของการติดตาม


    void Update()
    {
        // ตรวจสอบว่ามีตัวผู้เล่นหรือไม่
        if (player == null) return; // ออกหากไม่พบ Player

        // สร้างตำแหน่งเป้าหมายที่กล้องต้องการเคลื่อนไป
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        // เคลื่อนที่กล้องไปยังตำแหน่งเป้าหมายอย่างนุ่มนวลด้วย Lerp
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothNess * Time.deltaTime);
    }
}
