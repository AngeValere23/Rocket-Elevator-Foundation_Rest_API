using System;
public class interventions
{
    public long? id { get; set; }
    public long? AuthorID_id { get; set; }
    public long? CustomerID_id { get; set; }
    public long? BuildingID_id  { get; set; }
    public long? BatteryID_id  { get; set; }
    public long? ColumnID_id  { get; set; }
     public long? EmployeeID_id  { get; set; }
    public long? ElevatorID_id  { get; set; }
    public DateTime? InterventionStart  { get; set; }
    public DateTime? InterventionEnd { get; set; }
    public string Result { get; set; }
    public string Report  { get; set; }
    public string Status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }

}